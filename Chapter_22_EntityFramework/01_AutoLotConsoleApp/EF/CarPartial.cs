namespace _01_AutoLotConsoleApp.EF
{
    public partial class Car
    {
        public override string ToString() => $"{CarNickName ?? "**No Name**"} is a {Color}, {Mark}, Id = {CarId}";
    }
}