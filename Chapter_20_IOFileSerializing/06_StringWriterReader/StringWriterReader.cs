using System;
using System.IO;

namespace _06_StringWriterReader
{
    internal class StringWriterReader
    {
        private static void Main()
        {
            Console.Title = "Работа с классом StreamWriter/Reader";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************StreamWriter/Reader test app**************\n");
            using (var strWriter = new StringWriter())
            {
                strWriter.WriteLine("Text Message");
                Console.WriteLine($"Содержимое: {strWriter}");
                var sb = strWriter.GetStringBuilder();
                sb.Insert(0, "Val");
                Console.WriteLine(sb);
                sb.Remove(0, "Val".Length);
                Console.WriteLine(sb);
                using var strReader = new StringReader(strWriter.ToString());
                string input;
                while ((input = strReader.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
