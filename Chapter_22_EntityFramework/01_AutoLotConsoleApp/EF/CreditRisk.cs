namespace _01_AutoLotConsoleApp.EF
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CreditRisk")]
    public partial class CreditRisk
    {
        [Key]
        public int CustId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string SecondName { get; set; }
    }
}
