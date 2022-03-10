using System;
using System.IO;

namespace _07_BinaryWriterReader
{
    internal class BinaryWriterReader
    {
        private static void Main()
        {
            Console.Title = "Работа с классом BinaryWriter/Reader";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************BinaryWriter/Reader test app**************\n");
            var file = new FileInfo(@".\data.sp1");
            using (var binWriter = new BinaryWriter(file.Create()))
            {
                Console.WriteLine($"Базовый поток: {binWriter.BaseStream}");
                const double aDouble = 123.456;
                const int aInt = 123456;
                const string str = "A, B, C";
                binWriter.Write(aDouble);
                binWriter.Write(aInt);
                binWriter.Write(str);
            }
            using var binReader = new BinaryReader(file.OpenRead());
            {
                Console.WriteLine(binReader.ReadDouble());
                Console.WriteLine(binReader.ReadInt32());
                Console.WriteLine(binReader.ReadString());
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
