using System.Collections.Generic;

namespace _02_AutoLotDal.EF
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public int CustId { get; set; }

        public int CarId { get; set; }

        public virtual Custumer Custumer { get; set; }

        public virtual Inventory Car { get; set; }

    }
}
