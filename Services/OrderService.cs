using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services
{
    public class OrderService : IOrderService
    {
        private readonly ToTableDbContext _context;

        public OrderService(ToTableDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderItems()
        {
            return await _context.OrderItems
                .Include(o => o.Waiter)
                .Include(o => o.Payment)
                .Include(o => o.Table)
                .ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.OrderItems
                .Include(o => o.Waiter)
                .Include(o => o.Payment)
                .Include(o => o.Table)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task PostOrder(Order order)
        {
            // Wstawianie nowego zamówienia
            _context.OrderItems.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                throw new ArgumentException("Invalid Order ID");
            }

            // Aktualizacja zamówienia
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    throw new InvalidOperationException("Order not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteOrder(int id)
        {
            // Usunięcie zamówienia
            var order = await _context.OrderItems.FindAsync(id);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            _context.OrderItems.Remove(order);
            await _context.SaveChangesAsync();
        }

        private bool OrderExists(int id)
        {
            return (_context.OrderItems?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
