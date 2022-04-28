using System.ComponentModel.DataAnnotations.Schema;

namespace _02_AutoLotDal.EF
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Custumer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Custumer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int CustId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        [NotMapped] 
        public string FullName => $"{FirstName} + {LastName}";
    }
}
