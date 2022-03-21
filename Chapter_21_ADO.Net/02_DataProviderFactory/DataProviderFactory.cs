using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace _02_DataProviderFactory
{
    internal class DataProviderFactory
    {
        private static void Main()
        {
            Console.Title = "Пример простого подключения к базе данных";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************Sql Connection Factory************\n");
            var provider = ConfigurationManager.AppSettings["Provider"];
            var connectionString = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
            var factory = DbProviderFactories.GetFactory(provider);
            using (var connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    ShowError("Ошибка подключения");
                    return;
                }

                Console.WriteLine($"Тип подключения {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                if (!(connection is SqlConnection sqlConnection))
                {
                    ShowError("Ошибка подключения");
                    return;
                }

                Console.WriteLine(sqlConnection.ServerVersion);

                //Создать объект команды
                var command = factory.CreateCommand();
                if (command == null)
                {
                    ShowError("Ошибка создания команды");
                    return;
                }
                Console.WriteLine($"Тип команды {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Inventory";
                using (var dataReader = command.ExecuteReader())
                {
                    Console.WriteLine($"Тип reader {dataReader.GetType().Name}");
                    Console.WriteLine("Таблица Inventory");
                    while(dataReader.Read())
                        Console.WriteLine($"\t Car#{dataReader["CarId"]} is a {dataReader["Mark"]} цвет {dataReader["Color"]}");
                }
            }
            Console.WriteLine("\n************Работа приложения завершена************");
            Console.ReadLine();
        }

        private static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка программы: {message}");
            Console.ReadLine();
        }
    }
}
