using System;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace _10_DataParallelismWithForEach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //Экземпляр для выполнения отмены
        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CmdCancel_Click(object sender, RoutedEventArgs e)
        {
            _cancellationToken.Cancel();
        }

        private void CmdProcess_Click(object sender, RoutedEventArgs e)
        {
            ProcessFiles();
        }
        
        private void CmdProcessParallel_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(ProcessFilesParallel);
            
        }

        private void ProcessFiles()
        {
            //Загрузить все файлы с расширением .jpg
            var files = Directory.GetFiles(@"..\..\.\Images", "*.jpg", SearchOption.AllDirectories);
            //Создать директорию под выходные файлы
            const string newDir = @".\RotatedImages";
            if (Directory.Exists(newDir)) Directory.Delete(newDir, true);
            Directory.CreateDirectory(newDir);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                using (var bitmap = new Bitmap(file ?? throw new InvalidOperationException()))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, fileName));
                    //Вывести идентификатор потока, обрабатывающего текущее изображение
                    ViewLabel.Content = $"Обработка {fileName} в потоке {Thread.CurrentThread.ManagedThreadId}";
                }
                ViewLabel.Content = "Файлы успешно обработаны";
            }
        }
        private void ProcessFilesParallel()
        {
            //Создать класс для хранения токена отмены
            var parOpts = new ParallelOptions()
            {
                CancellationToken = _cancellationToken.Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
            //Загрузить все файлы с расширением .jpg
            var files = Directory.GetFiles(@"..\..\.\Images", "*.jpg", SearchOption.AllDirectories);
            //Создать директорию под выходные файлы
            const string newDir = @".\RotatedImages";
            if (Directory.Exists(newDir)) Directory.Delete(newDir, true);
            Directory.CreateDirectory(newDir);
            try
            {
                Parallel.ForEach(files, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();
                    var fileName = Path.GetFileName(currentFile);
                    using (var bitmap = new Bitmap(currentFile ?? throw new InvalidOperationException()))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, fileName));
                        //Для отображения результата выполнения метода необходимо использовать класс Dispatcher
                        Dispatcher.Invoke(delegate
                        {
                            ViewLabel.Content = $"Обработка {fileName} в потоке {Thread.CurrentThread.ManagedThreadId}";
                        });
                    }
                });
                Dispatcher.Invoke(() => { ViewLabel.Content = "Файлы успешно обработаны"; });
            }
            catch (OperationCanceledException e)
            {
                Dispatcher.Invoke(() => { ViewLabel.Content = $"Ошибка программы: {e.Message}"; });
            }
        }

    }
}
