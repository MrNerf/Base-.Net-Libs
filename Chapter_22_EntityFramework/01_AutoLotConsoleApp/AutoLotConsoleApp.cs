using System;
using System.Collections.Generic;
using System.Linq;
using _01_AutoLotConsoleApp.EF;

namespace _01_AutoLotConsoleApp
{
    internal class AutoLotConsoleApp
    {
        private static void Main()
        {
            Console.Title = "Базовый пример использования EF";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***********Entity Framework Test App***********\n");
            while (true)
            {
                Console.WriteLine("Главное меню программы:\n" +
                                  "Введите команду:\n" +
                                  "1 - добавить элемент в таблицу Inventory\n" +
                                  "2 - добавить множество элементов в таблицу Inventory\n" +
                                  "3 - Прочитать все элементы из таблицы Inventory\n" +
                                  "4 - Прочитать все элементы из таблицы Inventory управляемым запросом\n" +
                                  "5 - Построение запросов с помощью Linq\n" +
                                  "0 - Выход из программы");
                Console.Write("Command: ");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        var returnId = AddCar();
                        Console.WriteLine(returnId > 0 ? $"Id = {returnId}" : "Возникла ошибка");
                        break;
                    case "2":
                        var carMas = new Car[5];
                        for (var i = 0; i < carMas.Length; i++)
                        {
                            carMas[i] = new Car()
                            {
                                CarNickName = $"Ник {i}",
                                Color = $"Цвет {i}",
                                Mark = $"Марка {i}"
                            };
                            AddManyCar(carMas);
                        }
                        break;
                    case "3":
                        PrintAllInventory();
                        break;
                    case "4":
                        PrintAllInventorySql();
                        break;
                    case "5":
                        LinqQuery();
                        break;
                    case "0":
                        Console.WriteLine("***********Работа приложения завершена***********");
                        //Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }
        }

        private static void LinqQuery()
        {
            using (var context = new AutoLotEntities())
            {
                try
                {
                    Console.WriteLine("Вывести только марку BMW");
                    foreach (var car in context.Cars.Where(c => c.Mark == "BMW")) Console.WriteLine(car);

                    Console.WriteLine("Вывести только четные элементы + отсортированные по имени владельца");
                    foreach (var car in context.Cars.Where(c => c.CarId % 2 == 0).OrderBy(c => c.CarNickName))
                        Console.WriteLine(car);

                    Console.WriteLine("Получить проекцию данных по полю NickName");
                    foreach (var nickName in context.Cars.Select(n => new
                    {
                        n.Mark, n.Color
                    })) Console.WriteLine(nickName);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка программы {e.Message}");
                }
            }
        }

        private static void PrintAllInventorySql()
        {
            using (var autoLotContext = new AutoLotEntities())
            {
                try
                {
                    foreach (var car in autoLotContext.Cars.SqlQuery("Select CarId, Mark, Color, PetName as CarNickName from Inventory where Mark = @p0", "BMW"))
                    {
                        Console.WriteLine(car);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка программы {e.Message}");
                }
            }
        }

        private static int AddCar()
        {
            using (var autoLotContext = new AutoLotEntities())
            {
                try
                {
                    var car = new Car() {CarNickName = "Sonic", Color = "Blue", Mark = "Hedgehog"};
                    autoLotContext.Cars.Add(car);
                    autoLotContext.SaveChanges();
                    return car.CarId;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка программы {e.Message}");
                    return 0;
                }
            }
        }

        private static void AddManyCar(IEnumerable<Car> cars)
        {
            using (var autoLotContext = new AutoLotEntities())
            {
                try
                {
                    autoLotContext.Cars.AddRange(cars);
                    autoLotContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка программы {e.Message}");
                }
            }
        }

        private static void PrintAllInventory()
        {
            using (var autoLotContext = new AutoLotEntities())
            {
                try
                {
                    foreach (var car in autoLotContext.Cars)
                    {
                        Console.WriteLine(car);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка программы {e.Message}");
                }
            }
        }
    }
}
