using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace _01_ConnectionFactory
{
    internal class ConnectionFactory
    {
        private enum DataProvider
        {
            SqlServer,
            OleDb,
            Odbc,
            None
        }
        private static void Main()
        {
            Console.Title = "Пример простого подключения к базе данных";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************Simple Connection Factory************\n");
            var myConnection = GetConnection(DataProvider.SqlServer);
            Console.WriteLine($"Подключение: {myConnection.GetType().Name}");
            var provider = ConfigurationManager.AppSettings["provider"];
            if (Enum.IsDefined(typeof(DataProvider), provider))
            {
                var dataProvider = (DataProvider) Enum.Parse(typeof(DataProvider), provider);
                myConnection = GetConnection(dataProvider);
                Console.WriteLine($"Подключение: {myConnection.GetType().Name}");
            }
            else
            {
                Console.WriteLine("Неверно задан тип подключения в файле конфигурации");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("\n************Работа приложения завершена************");
            Console.ReadLine();
        }

        private static IDbConnection GetConnection(DataProvider dataProvider)
        {
            switch (dataProvider)
            {
                case DataProvider.SqlServer:
                    return new SqlConnection();
                case DataProvider.OleDb:
                    return new OleDbConnection();
                case DataProvider.Odbc:
                    return new OdbcConnection();
                case DataProvider.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataProvider), dataProvider, null);
            }

            return null;
        }
    }
}
