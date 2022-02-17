using System;
using System.Threading;

namespace _09_ThreadPool
{
    internal class ThreadPoolEx
    {
        private static void Main()
        {
            Console.Title = "Пример использования пула токов";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Thread pool test*************\n");
            var printer = new Printer();
            var workItemCb = new WaitCallback(PrintCb);
            for (var i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItemCb, printer);
            }

            Console.WriteLine("Все потоки завершены");
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.ReadLine();
        }

        private static void PrintCb(object state)
        {
            if (!(state is Printer printer)) throw new NotImplementedException();
            printer.PrintNumbers();
        }
    }
}
