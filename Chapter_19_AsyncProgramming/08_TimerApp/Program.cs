﻿using System;
using System.Threading;

namespace _08_TimerApp
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "Использование различных типов синхронизаций";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Various synchronization test*************\n");
            Console.WriteLine($"Id потока Main(): {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Введите команду:\n" +
                              "1 - пример использования типа Interlocked\n" +
                              "2 - пример использования атрибута Synchronization\n" +
                              "3 - пример использования типа Timer");
            a: Console.Write("Ваша команда ");
            var command = Console.ReadLine();
            var threadMas = new Thread[10];
            switch (command)
            {
                case "1":
                    Console.WriteLine("Данный пример необходимо смотреть в режиме отладки");
                    var interlockedEx = new InterlockedExample();
                    for (var i = 0; i < 10; i++) threadMas[i] = new Thread(interlockedEx.AddOne) {Name = $"Thread {i}"};
                    foreach (var thread in threadMas) thread.Start();
                    while (threadMas[9].IsAlive) Console.WriteLine($"Значение переменной {interlockedEx.IncValue}");
                    for (var i = 0; i < 10; i++) threadMas[i] = new Thread(interlockedEx.SubOne) { Name = $"Thread {i}" };
                    foreach (var thread in threadMas) thread.Start();
                    break;
                case "2":
                    
                    break;
                case "3":
                    
                    break;
                default:
                    Console.WriteLine("Введена неверная команда повторите попытку");
                    goto a;
            }

            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();
        }
    }
}