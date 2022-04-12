using System;
using System.Collections.Generic;
using System.Linq;
using _04_AutoLotDAL.BulkImport;
using _04_AutoLotDAL.DataOperations;
using _04_AutoLotDAL.Models;

namespace _05_AutoLotDalClient
{
    internal class AutoLotDalClient
    {
        private static void Main()
        {
            Console.Title = "Клиентское приложение для библиотеки AutoLotDal";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************Sql AutoLotDal Client Soft************\n");
            var inventoryDal = new InventoryDal();
            var inventoryList = inventoryDal.GetInventoryCars();
            int status;
            Console.WriteLine("Список автомобилей");
            Console.WriteLine("CarId\tMark\tColor\tPetName");
            foreach (var car in inventoryList)
                Console.WriteLine($"{car.CarId}\t{car.Mark}\t{car.Color}\t{car.PetName}");
            Continue();
            Console.WriteLine("\nПервый автомобиль сортированный по марке");
            var firstCar = inventoryDal.GetOneCars(inventoryList.OrderBy(x => x.Mark).Select(x => x.CarId).First());
            Console.WriteLine("CarId\tMark\tColor\tPetName");
            Console.WriteLine($"{firstCar.CarId}\t{firstCar.Mark}\t{firstCar.Color}\t{firstCar.PetName}");
            Continue();
            try
            {
                status = inventoryDal.DeleteAuto(5);
                if (status != 0) Console.WriteLine("\nЗапись успешно удалена");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Программа вернула ошибку {e.Message}");
            }

            Continue();
            Console.WriteLine("\nДобавление новой записи в таблицу");
            status = inventoryDal.InsertAuto(new Car() {Color = "Красный", Mark = "Amogus", PetName = "Амогус"});
            if (status != 0) Console.WriteLine("\nЗапись успешно Добавлена");
            inventoryList = inventoryDal.GetInventoryCars();
            Console.WriteLine("Список автомобилей");
            Console.WriteLine("CarId\tMark\tColor\tPetName");
            foreach (var car in inventoryList)
                Console.WriteLine($"{car.CarId}\t{car.Mark}\t{car.Color}\t{car.PetName}");
            Continue();
            Console.WriteLine("Изменить имя одной строки");
            var updateCar = inventoryList.First(car => car.PetName == "Амогус");
            status = inventoryDal.UpdateAuto(updateCar.CarId, "Красный предатель");
            if (status != 0) Console.WriteLine("\nЗапись успешно изменена");
            inventoryList = inventoryDal.GetInventoryCars();
            Console.WriteLine("Список автомобилей");
            Console.WriteLine("CarId\tMark\tColor\tPetName");
            foreach (var car in inventoryList)
                Console.WriteLine($"{car.CarId}\t{car.Mark}\t{car.Color}\t{car.PetName}");
            Continue();
            Console.WriteLine("Получить дружественное имя 4й записи в таблице");
            var petName = inventoryDal.LookUpPetName(4);
            Console.WriteLine($"CarID = 3, PetName = {petName}");
            Continue();
            //Пример транзакции
            var stat = inventoryDal.ProcessCreditRisk(false, 2);
            if (stat) Console.WriteLine("Транзакция прошла успешно");
            stat = inventoryDal.ProcessCreditRisk(true, 4);
            if (!stat) Console.WriteLine("Транзакция не прошла");
            Continue();
            DoBulkCopy();
            Console.WriteLine("\n************Работа приложения завершена************");
            Console.ReadLine();
        }

        private static void DoBulkCopy()
        {
            Console.WriteLine("Массовое копирование");
            var cars = new List<Car>()
            {
                new Car() {Color = "Белый", Mark = "БМВ", PetName = "Петя"},
                new Car() {Color = "Синий", Mark = "КИА", PetName = "Вася"},
                new Car() {Color = "Красный", Mark = "Хонда", PetName = "Коля"},
                new Car() {Color = "Желтый", Mark = "Мерседес", PetName = "Наташа"},
                new Car() {Color = "Голубой", Mark = "Вольво", PetName = "Даша"}
            };
            ProcessBulkImport.ExecuteBulkImport(cars, "Inventory");
            var dal = new InventoryDal();
            var list = dal.GetInventoryCars();
            Console.WriteLine("Список автомобилей");
            Console.WriteLine("CarId\tMark\tColor\tPetName");
            foreach (var car in list)
                Console.WriteLine($"{car.CarId}\t{car.Mark}\t{car.Color}\t{car.PetName}");
        }

        private static void Continue()
        {
            Console.WriteLine("Нажмите Enter чтобы продолжить");
            Console.ReadLine();
        }
    }
}
