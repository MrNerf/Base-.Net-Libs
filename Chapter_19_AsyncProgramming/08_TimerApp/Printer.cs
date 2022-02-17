using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace _08_TimerApp
{
    /*
     * Атрибут [Synchronization] блокирует целый класс для обращения потоков
     * Блокируются все элементы класса
     * Данный прием необходимо применять с осторожностью
     */
    [Synchronization]
    public class Printer : ContextBoundObject
    {
        public void PrintNumbers(object color)
        {
            Console.ForegroundColor = (ConsoleColor)color;
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
