using System;
using System.Collections;
using System.Collections.Generic;

namespace Test2Example.Entities
{
    public class Confectionery
    {
        public int IdConfect { get; set; }
        public string Name { get; set; }
        public double PricePerItem { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Confectionery_Order> Conf_Orders { get; set; }
    }
}