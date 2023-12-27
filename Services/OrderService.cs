using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class OrderService : IOrderService
{
    private readonly ToTableDbContext _context;

    public OrderService(ToTableDbContext context)
    {
        _context = context;
    }

    public Task<List<Order>> GetOrderItems()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    public Task PostOrder(Order Order)
    {
        throw new NotImplementedException();
    }

    public Task PutOrder(int id, Order Order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}