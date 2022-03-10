using System;
using System.Configuration;
using System.IO;

namespace _08_FileWatcher
{
    internal class FileWatcher
    {
        private static void Main()
        {
            Console.Title = "Работа с классом FileSystemWatcher";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************FileSystemWatcher test app**************\n");
            //Путь к директории наблюдения указан в файле app.config
            var appReader = new AppSettingsReader();
            var filePath = (string)appReader.GetValue("FilePath", typeof(string));
            var fileWatcher = new FileSystemWatcher(filePath, "*.txt")
            {
                NotifyFilter = NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.FileName |
                               NotifyFilters.DirectoryName |
                               NotifyFilters.Attributes
            };

            fileWatcher.Changed += OnChanged;
            fileWatcher.Created += OnChanged;
            fileWatcher.Deleted += OnChanged;
            fileWatcher.Renamed += FileWatcher_Renamed;

            fileWatcher.EnableRaisingEvents = true;
            Console.WriteLine("Для выхода из приложения нажмите q");
            while (Console.Read() != 'q')
            {
            }
            Console.WriteLine("\n**************Работа приложения завершена**************");
            Console.ReadLine();
        }

        private static void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Файл {e.OldName} был переименован. Новое имя {e.Name}");
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Был изменен файл {e.Name}, событие {e.ChangeType}, путь {e.FullPath}");
        }
    }
}
