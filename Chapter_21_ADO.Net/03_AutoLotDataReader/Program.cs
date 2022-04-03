using System;
using System.Data.SqlClient;

namespace _03_AutoLotDataReader
{
    internal class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Понятие подключенного уровня ADO.Net";
            Console.WriteLine("************Data Reader************\n");
            using (var sql = new SqlConnection())
            {
                //Символ . указывает, что используется локальная машина
                sql.ConnectionString = @"Data Source = .; Initial Catalog = AutoLot; Integrated Security = True; Connect Timeout = 30";
                sql.Open();
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
    }
}
