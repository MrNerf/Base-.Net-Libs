using System;
using System.IO;
using System.Text;

namespace _02_DriveInfo
{
    internal class MyDriveInfo
    {
        private static void Main()
        {
            Console.Title = "Работа с классом DriveInfo";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************DriveInfo test app**************\n");
            var sb = new StringBuilder();
            var myDrives = DriveInfo.GetDrives();
            foreach (var drive in myDrives)
            {
                sb.AppendLine($"Метка тома: {drive.Name}");
                sb.AppendLine($"Тип тома: {drive.DriveType}");
                if (drive.IsReady) // Проверка смонтирован ли диск
                {
                    sb.AppendLine($"Емкость тома 1: {drive.TotalFreeSpace / 1024 / 1024} Mb");
                    sb.AppendLine($"Емкость тома 2: {drive.AvailableFreeSpace / 1024 / 1024} Mb");
                    sb.AppendLine($"Емкость тома 3: {drive.TotalSize / 1024 / 1024} Mb");
                    sb.AppendLine($"Формат тома: {drive.DriveFormat}");
                    sb.AppendLine($"Дружественное имя тома: {drive.VolumeLabel}");
                }

                sb.AppendLine();
            }
            Console.WriteLine(sb);
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }
    }
}
