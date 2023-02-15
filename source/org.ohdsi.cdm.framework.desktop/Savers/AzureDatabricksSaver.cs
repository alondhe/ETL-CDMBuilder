using Microsoft.Azure.Databricks.Client;
using Npgsql;
using NpgsqlTypes;
using org.ohdsi.cdm.framework.common.Enums;
using org.ohdsi.cdm.framework.desktop.Enums;
using org.ohdsi.cdm.framework.desktop.Helpers;
using org.ohdsi.cdm.framework.common.Extensions;
using System;
using System.Data.Odbc;
using System.IO;
using System.Data;
using Parquet;
using System.Collections.Generic;
using Parquet.Data;
using DataColumn = Parquet.Data.DataColumn;
using System.Linq;

namespace org.ohdsi.cdm.framework.desktop.Savers
{
    public class AzureDatabricksSaver : Saver
    {
        private OdbcConnection _connection;
        private OdbcConnectionStringBuilder _odbcString;

        public override ISaver Create(string connectionString, CdmVersions cdmVersion, string sourceSchema, string destinationSchema)
        {
            CdmVersion = cdmVersion;
            SourceSchema = sourceSchema;
            DestinationSchema = destinationSchema;

            _odbcString = new OdbcConnectionStringBuilder(connectionString);
            _connection = SqlConnectionHelper.OpenOdbcConnection(connectionString);

            return this;
        }

        public override Database GetDatabaseType()
        {
            return Database.AzureDatabricks;
        }

        public override void Write(int? conversionId, int? chunkId, int? subChunkId, System.Data.IDataReader reader, string tableName)
        {
            if (reader == null)
                return;

            string fileName;
            if (tableName.ToLower() == "_chunks")
            {
                fileName = $"/FileStore/cdm/conversion_{conversionId}/chunks.parquet";
            }
            else
            {
                if(chunkId.HasValue)
                    fileName = $"/FileStore/cdm/conversion_{conversionId}/{tableName}_{chunkId}.parquet";
                else
                    fileName = $"/FileStore/cdm/conversion_{conversionId}/{tableName}.parquet";
            }

            var baseUrl = "https://" + _odbcString["host"].ToString();

            using (var client = DatabricksClient.CreateClient(baseUrl, _odbcString["pwd"].ToString()))
            using (var stream = GetStreamParquet(reader))
            {
                // Empty file
                if (stream.Length < 1000)
                    return;

                var upoad = client.Dbfs.Upload(fileName, true, stream);
                upoad.Wait();
            }

            if (tableName.ToLower().StartsWith("_chunks"))
            {
                tableName = SourceSchema + "." + tableName;
            }
            else
            {
                tableName = DestinationSchema + "." + tableName;
            }

            try
            {
                var sql = $"insert into {tableName} select * from parquet.`{fileName}`";
                using (var cmd = new OdbcCommand(sql, _connection))
                {
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                using (var client = DatabricksClient.CreateClient(baseUrl, _odbcString["pwd"].ToString()))
                using (var stream = GetStreamParquet(reader))
                {
                    var delete = client.Dbfs.Delete(fileName, true);
                    delete.Wait();
                }
            }
        }

        private MemoryStream GetStreamParquet(IDataReader reader)
        {
            const int rowGroupSize = 250 * 1000;
            var memoryStream = new MemoryStream();
            var schema = CreateSchema(reader);
            var fields = schema.GetDataFields();
            using (var parquet = new ParquetWriter(schema, memoryStream) { CompressionMethod = CompressionMethod.Gzip })
            {
                var cnt = 0;
                var data = new Dictionary<int, List<object>>();
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        if (!data.ContainsKey(i))
                            data.Add(i, new List<object>());

                        var value = reader.GetValue(i);

                        if (value == null || value is DBNull || value.ToString().Trim() == "\0" || string.IsNullOrEmpty(value.ToString()))
                            //if (value == null || value is DBNull)
                            data[i].Add(null);
                        else if (fields[i] is DateTimeDataField)
                        {
                            if (value is DateTimeOffset)
                                data[i].Add(value);
                            else
                            {
                                var dateTime = (DateTime)value;
                                if (dateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime)
                                {
                                    data[i].Add(DateTimeOffset.MinValue);
                                }
                                else if (dateTime.ToUniversalTime() >= DateTimeOffset.MaxValue.UtcDateTime)
                                {
                                    data[i].Add(DateTimeOffset.MaxValue);
                                }
                                else
                                {
                                    data[i].Add(new DateTimeOffset(dateTime));
                                }

                            }
                        }
                        else
                        {
                            data[i].Add(value is string ? value.ToString().Trim() : value);
                        }
                    }

                    cnt++;

                    if (cnt == rowGroupSize)
                    {
                        AddRowGroup(parquet, fields, data);

                        data.Clear();
                        cnt = 0;
                    }
                }

                if (cnt > 0)
                    AddRowGroup(parquet, fields, data);

                return memoryStream;
            }
        }

        private Schema CreateSchema(IDataReader reader)
        {
            var fields = new List<DataField>(reader.FieldCount);
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var t = reader.GetFieldType(i);
                var name = reader.GetName(i);

                if (t == typeof(int) || t == typeof(int?))
                {
                    fields.Add(new DataField(name, DataType.Int32, true, false));
                }
                else if (t == typeof(long) || t == typeof(long?))
                {
                    fields.Add(new DataField(name, DataType.Int64, true, false));
                }
                else if (t == typeof(decimal) || t == typeof(decimal?))
                {
                    fields.Add(new DecimalDataField(name, 38, 2, true));
                }
                else if (t == typeof(double) || t == typeof(double?))
                {
                    fields.Add(new DataField(name, DataType.Double, true, false));
                }
                else if (t == typeof(float) || t == typeof(float?))
                {
                    fields.Add(new DataField(name, DataType.Float, true, false));
                }
                else if (t == typeof(string))
                {
                    fields.Add(new DataField(name, DataType.String, true, false));
                }
                else if (t == typeof(DateTime) || t == typeof(DateTime?) ||
                         t == typeof(DateTimeOffset) || t == typeof(DateTimeOffset?))
                {
                    fields.Add(new DateTimeDataField(reader.GetName(i), DateTimeFormat.Date, true, false));
                }
                else if (t == typeof(TimeSpan) || t == typeof(TimeSpan?))
                {
                    fields.Add(new DataField(name, DataType.String, true, false));
                }
            }

            return new Schema(fields);
        }

        private void AddRowGroup(ParquetWriter parquet, DataField[] fields, Dictionary<int, List<object>> data)
        {
            using (var rgw = parquet.CreateRowGroup())
            {
                for (var i = 0; i < fields.Length; i++)
                {
                    var columData = data[i];
                    var dataField = fields[i];

                    switch (fields[i].DataType)
                    {
                        case DataType.Int32:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<int?>().ToArray()));
                            break;

                        case DataType.Int64:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<long?>().ToArray()));
                            break;

                        case DataType.Float:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<float?>().ToArray()));
                            break;

                        case DataType.Decimal:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<decimal?>().ToArray()));
                            break;

                        case DataType.Double:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<double?>().ToArray()));
                            break;

                        case DataType.String:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<string>().ToArray()));
                            break;

                        case DataType.DateTimeOffset:
                            rgw.WriteColumn(new DataColumn(dataField, columData.Cast<DateTimeOffset?>().ToArray()));
                            break;
                    }
                }
            }

        }

        public override void Commit()
        {
            //_transaction.Commit();
        }

        public override void Rollback()
        {
            //_transaction.Rollback();
        }

        public override void Dispose()
        {
            //_transaction.Dispose();
            _connection.Dispose();
        }
    }
}
