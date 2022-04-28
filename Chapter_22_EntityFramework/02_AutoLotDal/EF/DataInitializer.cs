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

            Database.SetInitializer(new DataInitializer());
        }
    }
}