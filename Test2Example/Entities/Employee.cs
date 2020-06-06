using System.Collections;
using System.Collections.Generic;

namespace Test2Example.Entities
{
    public class Employee
    {
        public int IdEmpl { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        
    }
}