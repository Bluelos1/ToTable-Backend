using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

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

    public async Task PostOrderItem(OrderItem OrderItem)
    {
        _context.OrderItemItems.Add(OrderItem);
        await _context.SaveChangesAsync();
    }

    public async Task PutOrderItem(int id, OrderItem OrderItem)
    {
        _context.Entry(OrderItem).State = EntityState.Modified;

    }

    public async Task DeleteOrderItem(int id)
    {
        var item = await _context.OrderItemItems.FindAsync(id);
        if (item == null)
        {
            _context.OrderItemItems.Remove(item);
            await _context.SaveChangesAsync();
        }

}
}