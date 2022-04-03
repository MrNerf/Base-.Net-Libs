using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace _03_AutoLotDataReader
{
    internal class AutoLotDataReader
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Понятие подключенного уровня ADO.Net";
            Console.WriteLine("************Data Reader************\n");
            var sqlConnectionSb = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "AutoLot",
                DataSource =  ".",
                IntegratedSecurity = true,
                ConnectTimeout = 15
            };
            using (var sql = new SqlConnection())
            {
                //Символ . указывает, что используется локальная машина
                sql.ConnectionString = sqlConnectionSb.ConnectionString;
                sql.Open();
                ShowConnectionStatus(sql);
                var sqlCommand = new SqlCommand("select * from Inventory;select * from Orders", sql);
                using (var dataReader = sqlCommand.ExecuteReader())
                {
                    do
                    {
                        Console.WriteLine("*********Начало записей в таблице*********");
                        while (dataReader.Read())
                        {
                            Console.WriteLine("*********Запись*********");
                            for (var i = 0; i < dataReader.FieldCount; i++)
                            {
                                Console.WriteLine($"-> {dataReader.GetName(i)} = {dataReader.GetValue(i)}");
                            }
                        }
                        Console.WriteLine("*********Конец записей в таблице*********");
                    } while (dataReader.NextResult());
                }
            }
            Console.WriteLine("\n************Работа приложения завершена************");
            Console.ReadLine();
        }

        private static void ShowConnectionStatus(DbConnection db)
        {
            Console.WriteLine("Информация о подключении");
            Console.WriteLine($"Расположение БД: {db.DataSource}");
            Console.WriteLine($"Имя БД: {db.Database}");
            Console.WriteLine($"Версия сервера: {db.ServerVersion}");
            Console.WriteLine($"Таймаут: {db.ConnectionTimeout}");
            Console.WriteLine($"Состояние: {db.State}");
        }
    }
}
