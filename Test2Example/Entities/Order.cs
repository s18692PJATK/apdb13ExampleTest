using System;
using System.Collections.Generic;

namespace Test2Example.Entities
{
    public class Order
    {
        public int IdOrder { get; set; }
        public DateTime DateAccepted { get; set; }
        public DateTime DateFinished { get; set; }
        public string Notes { get; set; }
        public int IdCustomer { get; set; }
        public int IdEmployee { get; set; }
        
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Confectionery_Order> Conf_Orders { get; set; }
        
    }
}