using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Test2Example.Entities;
using Test2Example.models;

namespace Test2Example.services
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly OrderContext _context;

        public OrderServiceImpl(OrderContext c)
        {
            _context = c;
        }

        public IEnumerable<OrderResponse> GetOrdersByName(string name)
        {
            var customers = GetCustomersByName(name);
            var orders = GetOrdersFromCustomers(customers);
            return CreateResponse(orders);
        }

        public IEnumerable<OrderResponse> GetOrders()
        {
            var customers = GetCustomers();
            var orders = GetOrdersFromCustomers(customers);
            return CreateResponse(orders);
        }

        public bool CreateOrder(OrderRequest orderRequest, int id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var customer = GetCustomer(id);
                var newOrder = new Order
                {
                    Customer = customer,
                    Notes = orderRequest.Notes,
                    DateAccepted = orderRequest.DateAccepted,
                    IdCustomer = id,
                };
                var confectioneries = GetConfectioneries(orderRequest.Products);
                var conf_orders = SaveConfOrders(confectioneries, newOrder);
                newOrder.Conf_Orders = conf_orders;
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        private ICollection<Confectionery_Order> SaveConfOrders(IEnumerable<Confectionery> confectioneries,
            Order newOrder)
        {
            var list =  confectioneries.Select(c => new Confectionery_Order
            {
                Confectionery = c,
                Order = newOrder
            }).ToList();
            _context.ConfectioneryOrders.AddRange(list);
            _context.SaveChanges();
            return list;
        }

        private Customer GetCustomer(int id)
        {
            return _context.Customers.First(c => c.IdClient == id);
        }

        private IEnumerable<Confectionery> GetConfectioneries(IEnumerable<ConfectioneryProduct> products)
        {
            var list = new List<Confectionery>();
            foreach (var product in products)
            {
                list.Add(_context.Confectioneries.First(c => c.Name.Equals(product.Name)));
                //this throws exception if none is found
            }

            return list;
        }

        private IEnumerable<OrderResponse> CreateResponse(IEnumerable<Order> orders)
        {
            return orders
                .Select(GetConfectionery)
                .Select(c => new OrderResponse
                {
                    Confectioneries = c
                })
                .ToList();
        }


        private ICollection<Customer> GetCustomersByName(string name)
        {
            return _context.Customers
                .Where(c => c.Name.Equals(name))
                .ToList();
        }

        private ICollection<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        private IEnumerable<Order> GetOrdersFromCustomers(ICollection<Customer> customers)
        {
            return _context.Orders
                .Where(c => customers.Contains(c.Customer))
                .ToList();
        }

        private IEnumerable<Confectionery> GetConfectionery(Order order)
        {
            return _context.Confectioneries
                .Where(c => c.Conf_Orders.Intersect(order.Conf_Orders).Any())
                .ToHashSet();
        }
    }
}