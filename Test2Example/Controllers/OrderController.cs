using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Test2Example.models;
using Test2Example.services;

namespace Test2Example.Controllers
{
    [ApiController]
    [Route("api/orders/")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("name")]
        public IActionResult GetOrders(string name)
        {
            IEnumerable<OrderResponse> list;
            if (name == null)
                list = _service.GetOrders();
            else list = _service.GetOrdersByName(name);

            if (list == null)
                return BadRequest("an error occured");
            else return Ok(list);
        } 
        [HttpPost("id")]
        public IActionResult CreateOrder(OrderRequest request, int id)
        {
            var success = _service.CreateOrder(request,id);
            if (success)
                return Ok("order was created");
            else return BadRequest("an error occured");
        }
    }
}