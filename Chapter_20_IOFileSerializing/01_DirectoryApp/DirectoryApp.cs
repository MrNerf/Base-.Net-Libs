using System;
using System.IO;
using System.Security.AccessControl;
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
            Console.WriteLine();
            DisplayImageFiles();
            Console.WriteLine();
            CreateNewDirectory();
            Console.WriteLine();
            DirectoryType();
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

        private static void DisplayImageFiles()
        {
            var dir = new DirectoryInfo(@"C:\Windows\Web\Wallpaper");
            var imageFiles = dir.GetFiles("*.jpg", SearchOption.AllDirectories);
            var sb = new StringBuilder();
            foreach (var imageFile in imageFiles)
            {
                sb.AppendLine($"############Информация о файле {imageFile.Name}############");
                sb.AppendLine($"Дата создания: {imageFile.CreationTime}");
                sb.AppendLine($"Время последнего обращения: {imageFile.LastAccessTime}");
                sb.AppendLine($"Вес файла: {(imageFile.Length)/1024}kB");
                sb.AppendLine($"############Конец информации о файле {imageFile.Name}############\n");
            }
            Console.WriteLine(sb);
        }

        private static void CreateNewDirectory()
        {
            var dir = new DirectoryInfo(".");
            dir.CreateSubdirectory("Logs", new DirectorySecurity(@".", AccessControlSections.Owner));
            var info = dir.CreateSubdirectory("Share");
            Console.WriteLine($"Новый каталог: {info.FullName}");

        }

        private static void DirectoryType()
        {
            var localDrives = Directory.GetLogicalDrives();
            Console.WriteLine("Список приводов");
            foreach (var drive in localDrives)
                Console.WriteLine($"--> {drive}");
            Console.WriteLine("Нажмите Enter чтобы удалить директорию Logs");
            Console.ReadLine();
            try
            {
                Directory.Delete(@".\Logs", true);
                Console.WriteLine("Директория удалена");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
