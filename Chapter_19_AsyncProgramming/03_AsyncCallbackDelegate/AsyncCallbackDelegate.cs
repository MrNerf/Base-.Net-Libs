using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace _03_AsyncCallbackDelegate
{
    public delegate int BinaryOp(int x, int y);
    internal class AsyncCallbackDelegate
    {
        /*
         * Переменная _isDone  необходима для удерживания первичного потока
         * Данный вариант не является безопасным в отношении потоков и служит
         * исключительно для иллюстрации процесса обработки потока с помощью
         * функции обратного вызова
         */
        private static bool _isDone;
        private static void Main()
        {
            Console.Title = "Использование асинхронного делегата AsyncCallback";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************AsyncCallbackExample*************\n");
            Console.WriteLine($"Функция Main выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            var binaryOp = new BinaryOp(Add);
            binaryOp.BeginInvoke(15, 35, AddCompleteCallback, "This message for AddCompleteCallback method");
            while (!_isDone)
            {
                Console.WriteLine($"Выполнение Main в потоке {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500);
            }
            
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();
        }

        private static void AddCompleteCallback(IAsyncResult iar)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Функция AddCompleteCallback выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            /*
             * Получение информации передаваемой в 4 аргументе функции binaryOp.BeginInvoke
             * Необходимо изначально знать принимаемый тип данных. По умолчанию System.Object
             */
            var asyncState = (string) iar.AsyncState;
            Console.WriteLine(asyncState);
            /*
             * Для вызова метода EndInvoke необходимо
             * получить объект класса AsyncResult
             * и из него получить объект делегата
             */
            var ar = (AsyncResult) iar;
            var bop = (BinaryOp) ar.AsyncDelegate;
            Console.WriteLine("Result is = {0}", bop.EndInvoke(iar));
            _isDone = true;
            Console.WriteLine("Выполнение потока завершено");
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private static int Add(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Функция Add выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(5000);
            Console.ForegroundColor = ConsoleColor.Green;
            return x + y;
        }
    }
}
