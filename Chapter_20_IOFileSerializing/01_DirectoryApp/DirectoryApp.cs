using System;
using System.IO;
using System.Text;

namespace _01_DirectoryApp
{
    internal class DirectoryApp
    {
        private static void Main()
        {
            Console.Title = "Получение информации о директории";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************Directory test app**************\n");
            ShowWindowsDirectoryInfo();
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }

        private static void ShowWindowsDirectoryInfo()
        {
            var dir = new DirectoryInfo(@"C:\Windows");
            var sb = new StringBuilder();
            sb.AppendLine($"############Информация о каталоге {dir.FullName}############");
            sb.AppendLine($"Название каталога: {dir.Name}");
            sb.AppendLine($"Родительский каталог: {dir.Parent}");
            sb.AppendLine($"Корневая часть каталога: {dir.Root}");
            sb.AppendLine($"Атрибуты каталога: {dir.Attributes}");
            sb.AppendLine($"Дата создания каталога: {dir.CreationTime}");
            sb.AppendLine($"Время последнего обращения к каталогу: {dir.LastAccessTime}");
            sb.AppendLine($"Время последнего обращения к каталогу на запись: {dir.LastWriteTime}");
            sb.AppendLine($"############Конец информации о каталоге {dir.FullName}############");
            Console.WriteLine(sb);
        }
    }
}
