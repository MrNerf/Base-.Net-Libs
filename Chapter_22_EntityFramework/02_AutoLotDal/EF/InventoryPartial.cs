using System.ComponentModel.DataAnnotations.Schema;

namespace _02_AutoLotDal.EF
{
    public partial class Inventory
    {
        //Атрибут означающий что поле не заполняется при чтении и не хранится в БД
        [NotMapped]
        public string MarkColor => $"{Mark} + {Color}";
        public override string ToString() => $"{PetName ?? "**No Name**"} is a {Color}, {Mark}, Id = {CarId}";
    }
}