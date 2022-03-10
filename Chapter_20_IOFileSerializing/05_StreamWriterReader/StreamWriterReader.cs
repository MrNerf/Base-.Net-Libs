using System;
using System.IO;

namespace _05_StreamWriterReader
{
    internal class StreamWriterReader
    {
        private static void Main()
        {
            Console.Title = "Работа с классом StreamWriter/Reader";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************StreamWriter/Reader test app**************\n");
            using (var streamWriter = File.CreateText(@".\Text.txt"))
            {
                streamWriter.WriteLine("Task 1");
                streamWriter.WriteLine("Task 2");
                for (var i = 0; i < 10; i++)
                    streamWriter.Write($"{i} ");
                streamWriter.Write(streamWriter.NewLine);
            }

            Console.WriteLine("Записанные данные в файл");
            using (var reader = File.OpenText(@".\Text.txt"))
            {
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
