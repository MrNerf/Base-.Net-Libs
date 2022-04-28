using System;
using System.Data.Entity;
using _02_AutoLotDal.EF;

namespace _03_AutoLotDalTest
{
    internal class AutoLotDalTest
    {
        private static void Main()
        {
            Console.Title = "Тест проекта 3";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***********Entity Framework Test App***********\n");
            //Database.SetInitializer(new DataInitializer());
            using (var context = new AutoLotEntities())
            {
                foreach (var inventory in context.Inventories)
                {
                    Console.WriteLine(inventory);
                }
            }
            Console.WriteLine("***********Работа приложения завершена***********");
            Console.ReadLine();
        }
    }
}
