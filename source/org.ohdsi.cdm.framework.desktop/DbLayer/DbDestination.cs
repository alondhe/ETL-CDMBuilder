﻿using org.ohdsi.cdm.framework.desktop.Databases;
using org.ohdsi.cdm.framework.desktop.Helpers;
using System;
using System.Data.Odbc;

namespace org.ohdsi.cdm.framework.desktop.DbLayer
{
    public class DbDestination
    {
        private readonly string _connectionString;
        private readonly string _schemaName;
        IDatabaseEngine _engine;

        public DbDestination(string connectionString, string schemaName)
        {
            _connectionString = connectionString;
            _schemaName = schemaName;

            if (_connectionString.ToLower().Contains("mysql"))
                _engine = new MySqlDatabaseEngine();
            else if (_connectionString.ToLower().Contains("postgres"))
                _engine = new PostgreDatabaseEngine();
            else if (_connectionString.ToLower().Contains("databricks"))
                _engine = new AzureDatabricksDatabaseEngine();
            else
                _engine = new MssqlDatabaseEngine();
        }

        public void CreateDatabase(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || string.IsNullOrEmpty(query))
                return;

            var sqlConnectionStringBuilder = new OdbcConnectionStringBuilder(_connectionString);
            var database = sqlConnectionStringBuilder["database"];

            // TMP
            var mySql = _connectionString.ToLower().Contains("mysql");

            if (_connectionString.ToLower().Contains("mysql"))
                sqlConnectionStringBuilder["database"] = "mysql";
            else if (_connectionString.ToLower().Contains("postgres"))
                sqlConnectionStringBuilder["database"] = "postgres";
            else if (_connectionString.ToLower().Contains("amazon redshift"))
                sqlConnectionStringBuilder["database"] = "poc";
            else
                sqlConnectionStringBuilder["database"] = "master";

            //using (var connection = SqlConnectionHelper.OpenOdbcConnection(sqlConnectionStringBuilder.ConnectionString))
            using (var connection = _engine.GetConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                query = string.Format(query, database);

                foreach (var subQuery in query.Split(new[] { "\r\nGO", "\nGO" }, StringSplitOptions.None))
                {
                    //using (var command = new OdbcCommand(subQuery, connection))
                    using (var command = _engine.GetCommand(subQuery, connection))
                    {
                        command.CommandTimeout = 30000;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void CreateSchema()
        {
            if (_connectionString.ToLower().Contains("mysql"))
                return;

            //using (var connection = SqlConnectionHelper.OpenOdbcConnection(_connectionString))
            using (var connection = _engine.GetConnection(_connectionString))
            {
                var query = $"create schema {_schemaName};";

                //using (var command = new OdbcCommand(query, connection))
                using (var command = _engine.GetCommand(query, connection))
                {
                    command.CommandTimeout = 0;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ExecuteQuery(string query)
        {
            //using (var connection = SqlConnectionHelper.OpenOdbcConnection(_connectionString))
            using (var connection = _engine.GetConnection(_connectionString))
            {
                query = query.Replace("{sc}", _schemaName);

                foreach (var subQuery in query.Split(new[] { "\r\nGO", "\nGO", ";" }, StringSplitOptions.None))
                {
                    if (string.IsNullOrEmpty(subQuery))
                        continue;

                    //using (var command = new OdbcCommand(subQuery, connection))
                    using (var command = _engine.GetCommand(subQuery, connection))
                    {
                        command.CommandTimeout = 30000;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
