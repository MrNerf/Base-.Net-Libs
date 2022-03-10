using System;
using System.IO;
using System.Text;

namespace _04_FileStreamApp
{
    internal class FileStreamApp
    {
        private static void Main()
        {
            Console.Title = "Работа с классом FileStream";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************FileStream test app**************\n");
            using (var fs = File.Open(@".\Message.dat", FileMode.Create))
            {
                Console.WriteLine("Введите строку, которую необходимо закодировать и записать в файл");
                Console.Write("Введенная строка: ");
                var str = Console.ReadLine();
                var strToBytes = Encoding.Default.GetBytes(str);
                fs.Write(strToBytes, 0, strToBytes.Length);
                Console.WriteLine("Данные записаны в файл\n");
                fs.Position = 0;
                Console.WriteLine("Данные полученные из файла");
                var readBytes = new byte[strToBytes.Length];
                for (var i = 0; i < strToBytes.Length; i++)
                {
                    readBytes[i] = (byte)fs.ReadByte();
                    Console.Write($"{readBytes[i]} ");
                }

                Console.WriteLine($"\nДекодированная строка: {Encoding.Default.GetString(readBytes)}");
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
        }
    }
}
