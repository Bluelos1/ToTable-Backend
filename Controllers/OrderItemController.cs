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
        public async Task<ActionResult<List<OrderItem>>> GetOrderObject()
        {
            var orderItem = _itemService.GetOrderItemObject();
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
        public async Task<ActionResult> PutOrder(int id, OrderItemDto orderItem)
        {
            if (id != orderItem.ItemId)
            {
                return BadRequest();
            }
            await _itemService.PutOrderItem(id, orderItem);
            return NoContent();
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
        public async Task<ActionResult<List<OrderItem>>> GetAllOrderObjectByOrderId(int orderId)
        {
            return await _itemService.GetAllOrderObjectByOrderId(orderId);
            
        }
        
        [HttpPost("Post")]
        public async Task<ActionResult<int>> AddProductToOrder(OrderItemDto orderItemDto)
        {
            await _itemService.PostOrderItem(orderItemDto);
            return CreatedAtAction("GetOrder", new { id = orderItemDto.ProductId }, orderItemDto);
        }

    
            [HttpPut("UpdateQuantity/{orderId}/{itemId}")]
    public async Task<IActionResult> UpdateOrderItemQuantity(int orderId, int itemId, int quantity)
    {
        var orderItem = await _itemService.UpdateOrderItemQuantity(orderId, itemId, quantity);
        return Ok(orderItem);
    }


     [HttpDelete("{orderId}/{productId}")]
    public async Task<ActionResult> DeleteOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        await _itemService.DeleteOrderItemByOrderIdAndProductId(orderId, productId);
        return NoContent();
    }


    }

   
}
