using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace _09_ThreadPool
{
    [Synchronization]
    public class Printer : ContextBoundObject
    {
        public void PrintNumbers()
        {
            var color = new Random();
            Console.ForegroundColor = (ConsoleColor)color.Next(1, 15);
            Console.WriteLine($"Название потока: {Thread.CurrentThread.Name}, Id потока: {Thread.CurrentThread.ManagedThreadId}\n");

            for (var i = 0; i < 10; i++)
            {
                var time = new Random();
                var delay = time.Next(1, 10);
                Thread.Sleep(100 * delay);
                Console.Write($"{i}\t");
                Console.WriteLine($"Потоковая задержка: {100 * delay}ms");
            }
            Console.WriteLine();
            Console.WriteLine($"Поток: {Thread.CurrentThread.Name} завершен");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
