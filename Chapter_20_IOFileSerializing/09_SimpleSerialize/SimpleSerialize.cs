using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

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
            SaveAsSoapFormat(jbc, "CarDataSoap.soap");
            LoadFromSoapFile("CarDataSoap.soap");
            SaveAsXmlFormat(jbc, "CarDataXml.xml");
            LoadFromXmlFile("CarDataXml.xml");
            SerializeCollection("Collection.xml");
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }

        private static void SerializeCollection(string path)
        {
            var jbcCollection = new List<JamesBondCar>()
            {
                new JamesBondCar(false, false),
                new JamesBondCar(false, true),
                new JamesBondCar(true, false),
                new JamesBondCar(true, true)
            };

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<JamesBondCar>));
                xmlSerializer.Serialize(fs, jbcCollection);
                Console.WriteLine("=> Файл сохранен в xml формат");
            }
        }

        #region Binary Serialization

        private static void LoadFromBinaryFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var binFormat = new BinaryFormatter();
                var jbc = (JamesBondCar)DeSerializeObjectGraph(binFormat, fs);
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

        #endregion

        #region Soap Serialization

        private static void SaveAsSoapFormat(JamesBondCar jbc, string path)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                var soapFormat = new SoapFormatter();
                SerializeObjectGraph(soapFormat, fs, jbc);
                Console.WriteLine("=> Файл сохранен в soap формат");
            }
        }

        private static void LoadFromSoapFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var soapFormat = new SoapFormatter();
                var jbc = (JamesBondCar)DeSerializeObjectGraph(soapFormat, fs);
                Console.WriteLine($"JamesBondCar:\tМожет летать: {jbc.CanFly}\n\t\t" +
                                  $"Может плавать: {jbc.CanSubmerge}\n\t\t" +
                                  $"Является хетчбеком: {jbc.IsHatchBack}\n\t\t" +
                                  $"Количество радиостанций: {jbc.TheRadio.StationPresets.Length}\n\t\t");

            }
        }

        #endregion

        #region XML Serialization

        private static void LoadFromXmlFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var xmlSerializer = new XmlSerializer(typeof(JamesBondCar));
                var jbc = (JamesBondCar)xmlSerializer.Deserialize(fs);
                Console.WriteLine($"JamesBondCar:\tМожет летать: {jbc.CanFly}\n\t\t" +
                                  $"Может плавать: {jbc.CanSubmerge}\n\t\t" +
                                  $"Является хетчбеком: {jbc.IsHatchBack}\n\t\t" +
                                  $"Количество радиостанций: {jbc.TheRadio.StationPresets.Length}\n\t\t");

            }
        }

        private static void SaveAsXmlFormat(JamesBondCar jbc, string path)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                var xmlSerializer = new XmlSerializer(typeof(JamesBondCar));
                xmlSerializer.Serialize(fs, jbc);
                Console.WriteLine("=> Файл сохранен в xml формат");
            }
        }

        #endregion

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
