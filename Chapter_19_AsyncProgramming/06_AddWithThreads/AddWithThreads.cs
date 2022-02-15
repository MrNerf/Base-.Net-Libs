using System;
using System.Threading;

namespace _06_AddWithThreads
{
    internal class AddWithThreads
    {
        //экземпляр класса для контроля над текущим потоком
        private static readonly AutoResetEvent WaitHandleResetEvent = new AutoResetEvent(false);
        private static void Main()
        {
            Console.Title = "Создание собственных потоков с параметром внутри приложения";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Create Parametrized Thread in App*************\n");
            Console.WriteLine($"Id потока Main(): {Thread.CurrentThread.ManagedThreadId}");
            var parameters = new AddParameters(150, 150);
            var paramThread = new Thread(Add) {Name = "Second Thread"};
            paramThread.Start(parameters);
            //ожидание пока не завершится второй поток
            WaitHandleResetEvent.WaitOne();
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();
        }

        private static void Add(object addParameters)
        {
            if (!(addParameters is AddParameters ap)) return;
            Console.WriteLine($"Название потока: {Thread.CurrentThread.Name}, Id потока: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("{0} + {1} = {2}", ap.A, ap.B, ap.B + ap.A);
            //Установка флага завершения второго потока
            WaitHandleResetEvent.Set();
        }
    }
    internal class AddParameters
    {
        public readonly int A, B;

        public AddParameters(int number1, int number2)
        {
            A = number1;
            B = number2;
        }
    }
}
