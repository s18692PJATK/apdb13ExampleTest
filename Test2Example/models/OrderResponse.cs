using System;
using System.Collections;
using System.Collections.Generic;
using Test2Example.Entities;

namespace Test2Example.models
{
    public class OrderResponse
    {
        public IEnumerable<Confectionery> Confectioneries { get; set; }
        
        
    }
}