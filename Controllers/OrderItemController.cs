using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ToTableDbContext _context;
        private readonly IOrderItemService _itemService;

        public OrderItemController(ToTableDbContext context, IOrderItemService itemService)
        {
            _context = context;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderItems()
        {
          if (_context.OrderItems == null)
          {
              return NotFound();
          }
            return await _context.OrderItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.OrderItems == null)
          {
              return NotFound();
          }
            var order = await _context.OrderItems.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.OrderItems == null)
          {
              return Problem("Entity set 'ToTableDbContext.OrderItems'  is null.");
          }
            _context.OrderItems.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/OrderItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.OrderItems == null)
            {
                return NotFound();
            }
            var order = await _context.OrderItems.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.OrderItems?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}