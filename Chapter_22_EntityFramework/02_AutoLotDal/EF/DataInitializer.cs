using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace _02_AutoLotDal.EF
{
    /// <summary>
    /// Класс будет удалять и воссоздавать БД каждый раз когда выполняется инициализатор
    /// </summary>
    public class DataInitializer : DropCreateDatabaseAlways<AutoLotEntities>
    {
        protected override void Seed(AutoLotEntities context)
        {
            var customers = new List<Custumer>
            {
                new Custumer() {FirstName = "Петя", LastName = "Васечкин"},
                new Custumer() {FirstName = "Коля", LastName = "Тупкин"},
                new Custumer() {FirstName = "Ваня", LastName = "Иванько"},
                new Custumer() {FirstName = "Женя", LastName = "Бурбон"},
                new Custumer() {FirstName = "Оля", LastName = "Картошкина"},
            };
            customers.ForEach(customer => context.Custumers.AddOrUpdate(c => new {c.FirstName, c.LastName}, customer));

            var cars = new List<Inventory>
            {
                new Inventory() {Mark = "БМВ", Color = "Белый", PetName = "Бэшка"},
                new Inventory() {Mark = "Мерседес", Color = "Черный", PetName = "Мерс"},
                new Inventory() {Mark = "Смарт", Color = "Желтый", PetName = "Смартик"},
                new Inventory() {Mark = "Вольво", Color = "Серый", PetName = "Вольво"},
                new Inventory() {Mark = "Опель", Color = "Красный", PetName = "Молния"},
                new Inventory() {Mark = "Порше", Color = "Розовый", PetName = "Порш"},
                new Inventory() {Mark = "Сааб", Color = "Белый", PetName = "Саб"},
                new Inventory() {Mark = "БМВ", Color = "Красный", PetName = "БэшкаКр"},
            };

            context.Inventories.AddOrUpdate(x=> new {x.Mark, x.Color}, cars.ToArray());

            var orders = new List<Order>
            {
                new Order() {Car = cars[0], Custumer = customers[0]},
                new Order() {Car = cars[1], Custumer = customers[1]},
                new Order() {Car = cars[2], Custumer = customers[2]},
                new Order() {Car = cars[3], Custumer = customers[3]},
            };

            orders.ForEach(order => context.Orders.AddOrUpdate(x=> new {x.CarId, x.CustId}, order));

            context.CreditRisks.AddOrUpdate(x=> new {x.FirstName, x.SecondName}, new CreditRisk()
            {
                CustId = customers[4].CustId,
                FirstName = customers[4].FirstName,
                SecondName = customers[4].LastName
            });

            Database.SetInitializer(new DataInitializer());
        }
    }
}