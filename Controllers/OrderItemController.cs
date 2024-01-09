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
    public class OrderItemController : ControllerBase
    {
        
        private readonly IOrderItemService _itemService;

        public OrderItemController(IOrderItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderItem>>> GetOrderItems()
        {
            var orderItem = _itemService.GetOrderItemItems();
          if (orderItem == null)
          {
              return NotFound();
          }

          return await orderItem;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrder(int id)
        {
          if (_itemService.GetOrderItem(id) == null)
          {
              return NotFound();
          }
            return await _itemService.GetOrderItem(id);
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult> PutOrder(int id, OrderItem orderItem)
        {
            if (id != orderItem.ItemId)
            {
                return BadRequest();
            }
            await _itemService.PutOrderItem(id, orderItem);
            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrder(OrderItem orderItem)
        {
            await _itemService.PostOrderItem(orderItem);
            return CreatedAtAction("GetOrder", new { id = orderItem.ItemId }, orderItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _itemService.GetOrderItem(id);
            if ( order == null)
            {
                return NotFound();
            }

            await _itemService.DeleteOrderItem(id);
            return NoContent();
        }

        [HttpGet("AllItems")]
        public async Task<ActionResult<List<OrderItem>>> GetAllOrderItemsByOrderId(int orderId)
        {
            return await _itemService.GetAllOrderItemsByOrderId(orderId);
            
        }
        
        [HttpPost("Product to order")]
        public async Task<ActionResult<int>> AddProductToOrder(OrderItemDto orderItemDto)
        {
             return await _itemService.AddProductToOrder(orderItemDto);
            return CreatedAtAction("GetOrder", new { id = orderItemDto.ProductId }, orderItemDto);

        }

       
    }
}
