using System.Collections.Generic;
using System.Data;

namespace _04_AutoLotDAL.BulkImport.Interfaces
{
    public interface IMyDataReader <T> : IDataReader
    {
        List<T> Records { get; set; }
    }
}