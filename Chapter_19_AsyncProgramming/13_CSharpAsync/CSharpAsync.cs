using System;
using System.Threading;
using System.Threading.Tasks;

namespace _13_CSharpAsync
{
    internal class CSharpAsync
    {
        private static async Task Main()
        {
            Console.Title = "Пример использования Parallel LINQ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Async/Await test*************\n");
            Console.WriteLine(DoWork());
            Console.WriteLine("Completed");
            Console.WriteLine(await DoWorkAsync());
            MultiTaskAsync().Wait();
            Console.WriteLine();
            var res = await ReturnValueTypeAsync();
            Console.WriteLine($"Async result value type {res}");
            Console.WriteLine("Completed Async");
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.ReadLine();
        }

        private static string DoWork()
        {
            Thread.Sleep(500);
            return "Work is done!";
        }

        private static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return "Work is done!";
            });
        }

        private static async Task MultiTaskAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1500);
                Console.WriteLine("Задача 1 завершена");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(1500);
                Console.WriteLine("Задача 2 завершена");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(1500);
                Console.WriteLine("Задача 3 завершена");
            });
        }

        /// <summary>
        /// Метод возвращающий значимый тип данных. Используется System.Threading.Tasks.Extensions из установщика NuGet
        /// </summary>
        /// <returns>Возвращает значимый тип данных</returns>
        private static async ValueTask<int> ReturnValueTypeAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(1500);
                Console.WriteLine("Задача завершена");
                return 10;
            });
        }
    }
}
