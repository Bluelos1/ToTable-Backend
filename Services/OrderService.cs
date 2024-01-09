using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class OrderService : IOrderService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<OrderService> _logger;
    public OrderService(ToTableDbContext context, ILogger<OrderService> logger )
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<Order>> GetOrderItems()
    {
        try
        {
            return _context.OrderItems.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Not found");
            throw;
        }
    }

    public Task<Order> GetOrder(int id)
    {
        return _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId == id);
    }

    public async Task PostOrder(Order Order)
    {
        _context.OrderItems.Add(Order);
        await _context.SaveChangesAsync();
    }

    public async Task PutOrder(int id, Order Order)
    {
        _context.Entry(Order).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(int id)
    {
        var item = await _context.OrderItems.FindAsync(id);
        if (item != null)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> OrderExists( int id)
    {
        return _context.OrderItems.AnyAsync(x => x.OrderId == id);
    }
    
    public async Task AddCommentToOrder(int orderId, string comment)
    {
        var order = await _context.OrderItems.FindAsync(orderId);
        if (order != null)
        {
            order.OrderComment = comment;
            await _context.SaveChangesAsync();
        }
    }
}