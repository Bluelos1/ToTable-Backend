using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services
{
    public class OrderItemService : IOrderItemItemService
    {
        private readonly ToTableDbContext _context;

        public OrderItemService(ToTableDbContext context)
        {
            _context = context;
        }

        public Task<List<OrderItem>> GetOrderItemItems()
        {
            return _context.OrderItemItems.ToListAsync();
        }

        public Task<OrderItem> GetOrderItem(int id)
        {
            return _context.OrderItemItems.FirstOrDefaultAsync(x => x.ItemId == id);
        }

        public async Task PostOrderItem(OrderItem orderItem)
        {
            _context.OrderItemItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task PutOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.ItemId)
            {
                
                throw new System.ArgumentException("Invalid OrderItem ID");
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                   
                    throw new System.InvalidOperationException("OrderItem not found");
                }
                else
                {
               
                    throw;
                }
            }
        }

        public async Task DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItemItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItemItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                
                throw new System.InvalidOperationException("OrderItem not found");
            }
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItemItems.Any(e => e.ItemId == id);
        }
    }
}
