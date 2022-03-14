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
            LoadFromBinaryFile("CarData.dat");
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }

        private static void LoadFromBinaryFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var binFormat = new BinaryFormatter();
                var jbc = (JamesBondCar) DeSerializeObjectGraph(binFormat, fs);
                Console.WriteLine($"JamesBondCar:\tМожет летать: {jbc.CanFly}\n\t\t" +
                                  $"Может плавать: {jbc.CanSubmerge}\n\t\t" +
                                  $"Является хетчбеком: {jbc.IsHatchBack}\n\t\t" +
                                  $"Количество радиостанций: {jbc.TheRadio.StationPresets.Length}\n\t\t");

            }
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

        private static object DeSerializeObjectGraph(IFormatter iFormatter, Stream dstStream)
        {
            return iFormatter.Deserialize(dstStream);
        }
    }
}
