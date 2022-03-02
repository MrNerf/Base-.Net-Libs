using System;
using System.IO;

namespace _03_FileIO
{
    internal class FileIo
    {
        private static void Main()
        {
            Console.Title = "Работа с классом File";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************File test app**************\n");
            var tasks = new [] {"Задача 1", "Задача 2", "Задача 3", "Задача 4" };
            File.AppendAllLines(@".\Tasks.dat", tasks);
            foreach (var line in File.ReadAllLines(@".\Tasks.dat"))
            {
                Console.WriteLine($"TODO: {line}");
            }
            
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
