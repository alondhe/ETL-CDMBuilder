using org.ohdsi.cdm.framework.common.Builder;
using org.ohdsi.cdm.framework.common.Omop;
using System;
using System.Collections.Generic;
using System.Data;

namespace org.ohdsi.cdm.framework.common.DataReaders.v5
{
    public class SpecimenDataReader : IDataReader
    {
        private readonly IEnumerator<Specimen> _enumerator;
        private readonly KeyMasterOffsetManager _offset;

        // A custom DataReader is implemented to prevent the need for the HashSet to be transformed to a DataTable for loading by SqlBulkCopy
        public SpecimenDataReader(List<Specimen> batch, KeyMasterOffsetManager o)
        {
            _enumerator = batch?.GetEnumerator();
            _offset = o;
        }

        public bool Read()
        {
            return _enumerator.MoveNext();
        }

        public int FieldCount
        {
            get { return 15; }
        }

        // is this called only because the datatype specific methods are not implemented?  
        // probably performance to be gained by not passing object back?
        public object GetValue(int i)
        {
            switch (i)
            {
                case 0:
                    return _offset.GetId(_enumerator.Current.PersonId, _enumerator.Current.Id);
                case 1:
                    return _enumerator.Current.PersonId;
                case 2:
                    return _enumerator.Current.ConceptId;
                case 3:
                    return _enumerator.Current.TypeConceptId;
                case 4:
                    return _enumerator.Current.StartDate;
                case 5:
                    return _enumerator.Current.StartDate;
                case 6:
                    return _enumerator.Current.Quantity;
                case 7:
                    return _enumerator.Current.UnitConceptId;
                case 8:
                    return _enumerator.Current.AnatomicSiteConceptId;
                case 9:
                    return _enumerator.Current.DiseaseConceptId;
                case 10:
                    return _enumerator.Current.SpecimenSourceId;
                case 11:
                    return _enumerator.Current.SourceValue;
                case 12:
                    return _enumerator.Current.UnitSourceValue;
                case 13:
                    return _enumerator.Current.AnatomicSiteSourceValue;
                case 14:
                    return _enumerator.Current.DiseaseSourceValue;

                default:
                    throw new NotImplementedException();
            }
        }

        public string GetName(int i)
        {
            switch (i)
            {
                case 0:
                    return "specimen_id";
                case 1:
                    return "person_id";
                case 2:
                    return "specimen_concept_id";
                case 3:
                    return "specimen_type_concept_id";
                case 4:
                    return "specimen_date";
                case 5:
                    return "specimen_datetime";
                case 6:
                    return "quantity";
                case 7:
                    return "unit_concept_id";
                case 8:
                    return "anatomic_site_concept_id";
                case 9:
                    return "disease_status_concept_id";
                case 10:
                    return "specimen_source_id";
                case 11:
                    return "specimen_source_value";
                case 12:
                    return "unit_source_value";
                case 13:
                    return "anatomic_site_source_value";
                case 14:
                    return "disease_status_source_value";

                default:
                    throw new NotImplementedException();
            }
        }

        #region implementationn not required for SqlBulkCopy

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            return (bool)GetValue(i);
        }

        public byte GetByte(int i)
        {
            return (byte)GetValue(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            return (char)GetValue(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)GetValue(i);
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)GetValue(i);
        }

        public double GetDouble(int i)
        {
            return Convert.ToDouble(GetValue(i));
        }

        public Type GetFieldType(int i)
        {
            switch (i)
            {
                case 0:
                    return typeof(long);
                case 1:
                    return typeof(long);
                case 2:
                    return typeof(int);
                case 3:
                    return typeof(int);
                case 4:
                    return typeof(DateTime);
                case 5:
                    return typeof(DateTime);
                case 6:
                    return typeof(decimal?);
                case 7:
                    return typeof(long?);
                case 8:
                    return typeof(long?);
                case 9:
                    return typeof(long?);
                case 10:
                    return typeof(string);
                case 11:
                    return typeof(string);
                case 12:
                    return typeof(string);
                case 13:
                    return typeof(string);
                case 14:
                    return typeof(string);

                default:
                    throw new NotImplementedException();
            }
        }

        public float GetFloat(int i)
        {
            return (float)GetValue(i);
        }

        public Guid GetGuid(int i)
        {
            return (Guid)GetValue(i);
        }

        public short GetInt16(int i)
        {
            return (short)GetValue(i);
        }

        public int GetInt32(int i)
        {
            return (int)GetValue(i);
        }

        public long GetInt64(int i)
        {
            return Convert.ToInt64(GetValue(i));
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            return (string)GetValue(i);
        }

        public int GetValues(object[] values)
        {
            var cnt = 0;
            for (var i = 0; i < FieldCount; i++)
            {
                values[i] = GetValue(i);
                cnt++;
            }

            return cnt;
        }

        public bool IsDBNull(int i)
        {
            return GetValue(i) == null;
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
