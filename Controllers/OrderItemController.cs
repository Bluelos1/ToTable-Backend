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
    public class OrderItemController : ControllerBase
    {
        
        private readonly IOrderItemService _itemService;

        public OrderItemController(IOrderItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderItemDto>>> GetOrderObject()
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
        public async Task<ActionResult> PutOrder(int id, OrderItemDto orderItem, IValidator<OrderItemDto> validator)
        {
            var validationResult = await validator.ValidateAsync(orderItem);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            if (id != orderItem.ItemId)
            {
                return BadRequest("The ID in the URL does not match the Item ID in the order item data.");
            }

            try
            {
                await _itemService.PutOrderItem(id, orderItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Order item with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the order item.");
            }

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
        public async Task<ActionResult<int>> AddProductToOrder(OrderItemDto orderItemDto, IValidator<OrderItemDto> validator)
        {
            await _itemService.PostOrderItem(orderItemDto);
            return CreatedAtAction("GetOrder", new { id = orderItemDto.ProductId }, orderItemDto);
        }

    
            [HttpPut("UpdateQuantity/{orderId}/{itemId}")]
    public async Task<IActionResult> UpdateOrderItemQuantity(int orderId, int itemId, int quantity)
    {
        if (quantity <= 0)
        {
            return BadRequest("Quantity must be greater than 0.");
        }

        try
        {
            var orderItem = await _itemService.UpdateOrderItemQuantity(orderId, itemId, quantity);
            if (orderItem == null)
            {
                return NotFound($"Order with ID {orderId} or Item with ID {itemId} not found.");
            }
            return Ok(orderItem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the order item quantity.");
        }
    }


     [HttpDelete("{orderId}/{productId}")]
    public async Task<ActionResult> DeleteOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        await _itemService.DeleteOrderItemByOrderIdAndProductId(orderId, productId);
        return NoContent();
    }


    }

   
}
