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
                var sqlCommand = new SqlCommand("select * from Inventory", sql);
                using (var dataReader = sqlCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"-> {dataReader["Mark"]}, " +
                                          $"{dataReader["Color"]}, " +
                                          $"{dataReader["PetName"]}");
                    }
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
