using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace _10_CustomSerialization
{
    internal class CustomSerialization
    {
        private static void Main()
        {
            Console.Title = "Пример собственной сериализации объектов";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************Custom Serialize test app**************\n");
            using (var fileStream = new FileStream("log.soap", FileMode.Create, FileAccess.Write)) 
            {
                var stringClass = new StringData();
                var sf = new SoapFormatter();
                sf.Serialize(fileStream, stringClass);
                
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
