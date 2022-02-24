using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _12_PLINQ_DataProcessing
{
    internal class PlinqDataProcessing
    {
        private static readonly CancellationTokenSource CancellationToken = new CancellationTokenSource();
        private static void Main()
        {
            Console.Title = "Пример использования Parallel LINQ";
            Console.ForegroundColor = ConsoleColor.Green;
            while (true)
            {
                Console.WriteLine("*************Parallel LINQ test*************\n");
                Console.WriteLine("Нажмите Enter чтобы продолжить");
                Console.ReadLine();
                Console.WriteLine("Обработка...\n");
                Task.Factory.StartNew(ProcessIntData);
                Console.WriteLine("Для отмены обработки введите Q");
                var answer = Console.ReadLine();
                if (answer == null || !answer.Equals("Q", StringComparison.OrdinalIgnoreCase)) continue;
                CancellationToken.Cancel();
                break;
            }
            

            Console.WriteLine("\nРабота приложения завершена\n********************************************");
            Console.ReadLine();
        }

        private static void ProcessIntData()
        {
            var mas = Enumerable.Range(1, 100_000_000).ToArray();
            try
            {
                var modZero = mas.AsParallel().WithCancellation(CancellationToken.Token).Where(element => element % 33 == 0).OrderByDescending(element => element)
                    .Select(element => element).ToArray();
                Console.WriteLine($"Найдено {modZero.Length} значений");
            }
            catch (OperationCanceledException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
