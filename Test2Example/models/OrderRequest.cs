using System;
using System.Collections;
using System.Collections.Generic;
using Test2Example.Entities;

namespace Test2Example.models
{
    public class OrderRequest
    {
        public DateTime DateAccepted { get; set; }
        public string Notes { get; set; }
        public IEnumerable<ConfectioneryProduct> Products { get; set; }
        
        
    }
}