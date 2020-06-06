using System.Collections;
using System.Collections.Generic;
using Test2Example.Entities;
using Test2Example.models;

namespace Test2Example.services
{
    public interface IOrderService
    {
        public IEnumerable<OrderResponse> GetOrdersByName(string name);
        public IEnumerable<OrderResponse> GetOrders();
        public bool CreateOrder(OrderRequest orderRequest, int id);

    }
}