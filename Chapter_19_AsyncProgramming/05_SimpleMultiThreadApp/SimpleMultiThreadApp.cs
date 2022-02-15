using System;
using System.Threading;
using System.Windows.Forms;

namespace _05_SimpleMultiThreadApp
{
    internal class SimpleMultiThreadApp
    {
        private static void Main()
        {
            Console.Title = "Создание собственных потоков внутри приложения";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Create Thread in App*************\n");
            Thread.CurrentThread.Name = "Main program thread";
            Console.WriteLine("Для вызова метода PrintNumbers() в основном потоке введите команду 1\n" +
                              "Для вызова метода PrintNumbers() в другом потоке введите 2");
            Console.Write("Введите команду = ");
            var command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    PrintNumbers();
                    break;
                case "2":
                    var secondThread = new Thread(PrintNumbers);
                    secondThread.Name = "Second program thread";
                    secondThread.Start();
                    break;
                default:
                    Console.WriteLine("Ошибка ввода программа будет остановлена");
                    break;
            }

            MessageBox.Show("Программа продолжает работать, вычисление проходит в другом потоке", null, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();
        }

        private static void PrintNumbers()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Метод PrintNumbers() выполняется в потоке с именем: {Thread.CurrentThread.Name}");

            Console.WriteLine("Print Numbers:");
            for (var i = 0; i < 20; i++)
            {
                Console.Write($"{i}\t");
                Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.WriteLine("End of PrintNumbers() method");
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
