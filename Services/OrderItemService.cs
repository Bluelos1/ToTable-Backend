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
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetOrderItem(int id)
    {
        throw new NotImplementedException();
    }

    public Task PostOrderItem(OrderItem OrderItem)
    {
        throw new NotImplementedException();
    }

    public Task PutOrderItem(int id, OrderItem OrderItem)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderItem(int id)
    {
        throw new NotImplementedException();
    }
}