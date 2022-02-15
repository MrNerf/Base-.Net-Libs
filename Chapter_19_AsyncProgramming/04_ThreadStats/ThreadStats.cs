using System;
using System.Threading;

namespace _04_ThreadStats
{
    internal class ThreadStats
    {
        private static void Main()
        {
            Console.Title = "Изучение базовых свойств класса System.Threading";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************System.Threading stats*************\n");
            var primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary thread of ThreadStats application";
            //Показать детали обслуживающего домена и контекста
            Console.WriteLine($"Название текущего домена: {Thread.GetDomain().FriendlyName}\n" +
                              $"ID текущего контекста: {Thread.CurrentContext.ContextID}");
            Console.WriteLine("");
            //Вывод статистики о текущем потоке
            Console.WriteLine($"Имя потока: {primaryThread.Name}\n" +
                              $"Запущен ли поток: {primaryThread.IsAlive}\n" +
                              $"Выполняется ли в фоновом режиме: {primaryThread.IsBackground}\n" +
                              $"Приоритет потока: {primaryThread.Priority}\n" +
                              $"Состояние потока: {primaryThread.ThreadState}");
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();
        }
    }
}
