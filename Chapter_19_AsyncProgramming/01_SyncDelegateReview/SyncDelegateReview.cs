using System;
using System.Threading;

namespace SyncDelegateReview
{
    internal class SyncDelegateReview
    {
        public delegate int BinaryOp(int x, int y);

        private static void Main()
        {
            Console.Title = "Вызов делегата в блокирующем режиме";
            Console.ForegroundColor = ConsoleColor.Green;
            //вывод идентификатора выполняемого потока
            Console.WriteLine($"Функция Main() выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            var b = new BinaryOp(Add);
            var answer = b.Invoke(10, 10);
            //Следующие строки не выполнятся до завершения выполнения метода Add(10,10)
            Console.WriteLine("----------------------------");
            Console.WriteLine($"10 + 10 = {answer}");
            Console.WriteLine("Работа приложения завершена");
            Console.Read();
        }

        private static int Add(int x, int y)
        {
            Console.WriteLine($"Функция Add(int x, int y) выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            return x + y;
        } 
    }
}
