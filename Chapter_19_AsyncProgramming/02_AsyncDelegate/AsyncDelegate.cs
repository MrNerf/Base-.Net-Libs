using System;
using System.Threading;

namespace _02_AsyncDelegate
{
    internal class AsyncDelegate
    {
        public delegate int BinaryOp(int x, int y);
        private static void Main()
        {
            Console.Title = "Вызов делегата асинхронно";
            Console.ForegroundColor = ConsoleColor.Green;
            //вывод идентификатора выполняемого потока
            Console.WriteLine($"Функция Main() выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            var b = new BinaryOp(Add);
            var asyncResult = b.BeginInvoke(10, 10, null, null);
            //-------------------------------------------------
            //Данные инструкции выполнятся т.к. вычисление результата выполняется в отдельном потоке
            //Пока поток вычисления результата не завершится будут выполняться следующие инструкции
            while (!asyncResult.AsyncWaitHandle.WaitOne(1000, false))
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Выполнение команд функции Main");
                Console.WriteLine("------------------------------");
            }
            //-------------------------------------------------
            var answer = b.EndInvoke(asyncResult);
            Console.WriteLine($"10 + 10 = {answer}");
            Console.WriteLine("Работа приложения завершена\n---------------------------------------");
            Console.Read();
        }

        private static int Add(int x, int y)
        {
            Console.WriteLine($"Функция Add(int x, int y) выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(10000);
            return x + y;
        }
    }
}
