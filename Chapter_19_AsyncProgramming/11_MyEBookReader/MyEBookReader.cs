using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _11_MyEBookReader
{
    internal class MyEBookReader
    {
        private static string _theBook;
        private static void Main()
        {
            Console.Title = "Пример использования Parallel.Invoke()";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************Parallel.Invoke() test*************\n");
            // ReSharper disable once StringLiteralTypo
            Console.WriteLine("Загрузка книги с открытого сайта Гутенберга\n");
            GetBook();

            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.ReadLine();
        }

        private static void GetBook()
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadStringCompleted += (sender, args) =>
                {
                    _theBook = args.Result;
                    Console.WriteLine("Загрузка книги завершена");
                    GetStats();
                };
                webClient.DownloadStringAsync(new Uri("https://gutenberg.org/files/64317/64317-0.txt"));
            }
        }

        private static void GetStats()
        {
            var words = _theBook.Split(new[] {' ', '\u000A', ',', '.', ';', ':', '-', '?', '!', '/', '—' }, StringSplitOptions.RemoveEmptyEntries);

            var tenMostCommonWords = FindMostCommonWords(words);
            var longestWord = FindLongestWord(words);

            var bookStats = new StringBuilder("10 ключевых слов книги:\n");
            foreach (var commonWord in tenMostCommonWords) bookStats.AppendLine(commonWord);

            bookStats.AppendFormat($"Самое длинное слово {longestWord}");
            bookStats.AppendLine();

            Console.WriteLine(bookStats.ToString(), "Информация о книге");
        }

        private static string FindLongestWord(string[] words)
        {
            if (words == null) throw new ArgumentNullException(nameof(words));
            return (words.OrderByDescending(word => word.Length).Select(word => word)).FirstOrDefault();
        }

        private static IEnumerable<string> FindMostCommonWords(IEnumerable<string> words)
        {
            var freqOrder = words.Where(word => word.Length > 6)
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key);
            return (freqOrder.Take(10)).ToArray();
        }
    }
}
