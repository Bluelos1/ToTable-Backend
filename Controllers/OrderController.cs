using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
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
            var order = await _orderService.GetOrderObject();
            if (order == null || !order.Any())
            {
                return NotFound();
            }

            return  Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var item = await _orderService.GetOrder(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto order, IValidator<OrderDto> validator)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var validationResult = await validator.ValidateAsync(order);
    
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            try
            {
                await _orderService.PutOrder(id, order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the order.");
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderDto order, IValidator<OrderDto> validator)
        {
            var result = await validator.ValidateAsync(order);
            if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage));

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
            if (string.IsNullOrWhiteSpace(comment))
            {
                return BadRequest("Comment cannot be empty.");
            }
            
            var orderExists = await _orderService.OrderExists(id);
            if (!orderExists)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            try
            {
                await _orderService.AddCommentToOrder(id, comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the comment.");
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

