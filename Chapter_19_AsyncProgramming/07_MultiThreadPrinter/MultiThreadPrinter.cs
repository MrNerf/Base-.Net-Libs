using System;
using System.Threading;
using System.Windows.Forms;

namespace _07_MultiThreadPrinter
{
    internal class MultiThreadPrinter
    {
        //Данное поле необходимо для блокировки потока для статических методов
        private static readonly object LockObject = new object();

        private static void Main()
        {
            Console.Title = "Проблемы параллелизма";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Trouble with thread resources*************\n");
            Console.WriteLine($"Id потока Main(): {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Введите команду:\n" +
                              "1 - пример фонового потока\n" +
                              "2 - пример проблем параллелизма за общие ресурсы\n" +
                              "3 - пример использования ключевого слова lock для блокировки ресурсов");
            a: Console.Write("Ваша команда ");
            var command = Console.ReadLine();
            var threadMas = new Thread[10];
            switch (command)
            {
                case "1":
                    var parameters = new AddParameters(150, 150);
                    var paramThread = new Thread(AddBackground) { Name = "Second Thread", IsBackground = true };
                    paramThread.Start(parameters);
                    //Если убрать метод Console.ReadLine() фоновый поток завершится после завершения приложения, вне зависимости от результата.
                    Console.ReadLine();
                    break;
                case "2":
                    Console.WriteLine("Пример конфликта за общие ресурсы");
                    for (var i = 0; i < threadMas.Length; i++)
                    {
                        threadMas[i] = new Thread(PrintNumbers) {Name = $"Второстепенный поток {i}"};
                    }
                    for (var i = 0; i < threadMas.Length; i++) threadMas[i].Start(i);
                    //Дождаться пока поток 9 завершится после вывести сообщение о завершении работы программы
                    //Данное решение может неверно работать для не синхронизированного метода
                    while (threadMas[9].IsAlive) { }
                    break;
                case "3":
                    Console.WriteLine("Пример использования ключевого слова Lock для блокировки вызываемого метода в текущем потоке");
                    for (var i = 0; i < threadMas.Length; i++)
                    {
                        threadMas[i] = new Thread(PrintNumbersLock) { Name = $"Второстепенный поток {i}" };
                    }
                    for (var i = 0; i < threadMas.Length; i++) threadMas[i].Start(i+1);
                    //Дождаться пока поток 9 завершится после вывести сообщение о завершении работы программы
                    while (threadMas[9].IsAlive) { }
                    break;
                default:
                    Console.WriteLine("Введена неверная команда повторите попытку");
                    goto a;
            }

            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.Read();

        }

        private static void AddBackground(object addParameters)
        {
            if (!(addParameters is AddParameters ap)) return;
            Console.WriteLine($"Название потока: {Thread.CurrentThread.Name}, Id потока: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            MessageBox.Show($"{ap.A} + {ap.B} = {ap.A + ap.B}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Метод для демонстрации проблемы параллелизма без использования блокировки потока
        /// </summary>
        /// <param name="color">Значение цвета выводимого в консоли. Цвет задается числом от 0 до 15</param>
        private static void PrintNumbers(object color)
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

        /// <summary>
        /// Метод для демонстрации проблемы параллелизма c использованием блокировки потока
        /// </summary>
        /// <param name="color">Значение цвета выводимого в консоли. Цвет задается числом от 0 до 15</param>
        private static void PrintNumbersLock(object color)
        {
            
            lock (LockObject)
            {
                Console.ForegroundColor = (ConsoleColor)color;
                Console.WriteLine($"Название потока: {Thread.CurrentThread.Name}, Id потока: {Thread.CurrentThread.ManagedThreadId}\n");

                for (var i = 0; i < 10; i++)
                {
                    var time = new Random();
                    var delay = time.Next(1,10);
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