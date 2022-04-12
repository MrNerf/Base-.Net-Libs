using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace _04_AutoLotDAL.BulkImport
{
    public static class ProcessBulkImport
    {
        #region Fields

        private const string _connectionString = @"Data Source = DRAGON; Initial Catalog = AutoLot; Integrated Security = True";

        private static SqlConnection _sqlConnection;

        #endregion

        #region Base Methods

        /// <summary>
        /// Открывает подключение к базе данных
        /// </summary>
        public static void OpenConnection()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();
        }

        /// <summary>
        /// Закрывает подключение к базе данных, если оно не было установлено
        /// </summary>
        public static void CloseConnection()
        {
            if (_sqlConnection?.State != ConnectionState.Closed)
            {
                _sqlConnection?.Close();
            }

        }

        public static void ExecuteBulkImport<T>(IEnumerable<T> records, string tableName)
        {
            OpenConnection();
            using (var connection = _sqlConnection)
            {
                var bulkCopy = new SqlBulkCopy(connection) {DestinationTableName = tableName};
                var dataReader = new MyDataReader<T>(records.ToList());
                try
                {
                    bulkCopy.WriteToServer(dataReader);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        #endregion
    }
}