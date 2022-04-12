using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using _04_AutoLotDAL.BulkImport.Interfaces;

namespace _04_AutoLotDAL.BulkImport
{
    public class MyDataReader <T> : IMyDataReader<T>
    {
        #region Not Implemented

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[int i] => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public int Depth { get; }
        public bool IsClosed { get; }
        public int RecordsAffected { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        private int _currentIndex = -1;
        private readonly PropertyInfo [] _propertyInfo;
        private readonly Dictionary<string, int> _nameDictionary;

        public MyDataReader(List<T> records)
        {
            Records = records;
            _propertyInfo = typeof(T).GetProperties();
            _nameDictionary = _propertyInfo.Select((x, index) => new {x.Name, index})
                .ToDictionary(pair => pair.Name, pair => pair.index);
        }

        public string GetName(int i) => i >= 0 && i < FieldCount ? _propertyInfo[i].Name : string.Empty;

        public object GetValue(int i) => _propertyInfo[i].GetValue(Records[_currentIndex]);

        public int GetOrdinal(string name) => _nameDictionary.ContainsKey(name) ? _nameDictionary[name] : -1;


        public int FieldCount => _propertyInfo.Length;

        public bool Read()
        {
            if ((_currentIndex + 1) >= Records.Count) return false;
            _currentIndex++;
            return true;
        }

        public List<T> Records { get; set; }
    }
}