using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderObject()
        {
            var order = _orderService.GetOrderObject();
            if (order == null)
            {
                return NotFound();
            }

            return await order;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var item = await _orderService.GetOrder(id);
            if (item == null)
            {
                return NotFound();
            }

            return await _orderService.GetOrder(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            await _orderService.PutOrder(id, order);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostOrder(OrderDto order)
        {
            await _orderService.PostOrder(order);
            return CreatedAtAction("GetOrder", new
            {
                id = order.OrderId
            }, order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrder(id);
            return NoContent();
        }

        [HttpPost("PostComment")]
        public async Task<IActionResult> AddCommentToOrder(int id, string comment)
        {
            var orderExists = await _orderService.OrderExists(id);
            if (orderExists)
            {
                _orderService.AddCommentToOrder(id, comment);
            }

            return Ok();
        }



        [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByRestaurantId(int restaurantId)
        {
            var orders = await _orderService.GetOrdersByRestaurantId(restaurantId);
            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }


    }
}

