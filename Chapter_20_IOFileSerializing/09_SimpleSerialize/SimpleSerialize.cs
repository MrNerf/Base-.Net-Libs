using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _09_SimpleSerialize
{
    internal class SimpleSerialize
    {
        private static void Main()
        {
            Console.Title = "Пример сериализации объектов";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************Serialize test app**************\n");
            var jbc = new JamesBondCar
            {
                CanFly = true,
                CanSubmerge = true,
                IsHatchBack = false,
                TheRadio =
                {
                    HasSubWoofers = true,
                    HasTweeters = false,
                    StationPresets = new[] {96.4, 101.7, 105.3, 106.8}
                }
            };
            SaveAsBinaryFormat(jbc, "CarData.dat");
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }

        private static void SaveAsBinaryFormat(JamesBondCar jbc, string path)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                var binFormat = new BinaryFormatter();
                SerializeObjectGraph(binFormat, fs, jbc);
                Console.WriteLine("=> Файл сохранен в двоичный формат");
            }
        }

        /// <summary>
        /// Метод сериализации объектов в binary или soap
        /// </summary>
        /// <param name="iFormatter">Тип сериализации</param>
        /// <param name="dstStream">Поток для сериализации</param>
        /// <param name="graph">Данные для сериализации</param>
        private static void SerializeObjectGraph(IFormatter iFormatter, Stream dstStream, object graph)
        {
            iFormatter.Serialize(dstStream, graph);
        }

        private static void DeSerializeObjectGraph(IFormatter iFormatter, Stream dstStream, object graph)
        {
            iFormatter.Deserialize(dstStream);
        }
    }
}
