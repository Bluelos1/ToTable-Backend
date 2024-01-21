using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class OrderService : IOrderService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<OrderService> _logger;
    private readonly IWaiterService _waiterService;

    public OrderService(ToTableDbContext context, ILogger<OrderService> logger, IWaiterService waiterService)
    {
        _context = context;
        _logger = logger;
        _waiterService = waiterService;
    }

    public Task<List<Order>> GetOrderObject()
    {
        try
        {
            return _context.OrderObject.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Not found");
            throw;
        }
    }


    public Task<List<Order>> GetOrderItems()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrder(int id)
    {
        return _context.OrderObject.FirstOrDefaultAsync(x => x.OrderId == id);
    }

    public async Task<int> PostOrder(Order order)
    {
        var waiterId = await _waiterService.GetAvailableWaiterId();
           
        var orderItem = new Order
        {
            OrderTime = DateTime.Now,
            OrderStatus = OrderStatus.New,
            OrderComment = null,
            WaiterId = waiterId,
            TableId = order.TableId,
            RestaurantId = order.RestaurantId
        }; 
        var waiter = await _context.WaiterObject.FindAsync(waiterId);
        if (waiter != null)
        {
            waiter.IsAvailable = false;
        } 
        
        _context.OrderObject.Add(orderItem);
        await _context.SaveChangesAsync();
        return order.OrderId;
    }

    public async Task PutOrder(int id, Order order)
    {
        var orderById = _context.OrderObject.FirstOrDefault(x => x.OrderId == id);

        orderById.OrderTime = DateTime.Now;
        orderById.OrderStatus = OrderStatus.New;
        orderById.OrderComment = null;
        orderById.WaiterId = order.WaiterId;
        orderById.TableId = order.TableId;
        orderById.RestaurantId = order.RestaurantId;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(int id)
    {
        var item = await _context.OrderObject.FindAsync(id);
        if (item != null)
        {
            _context.OrderObject.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> OrderExists(int id)
    {
        return _context.OrderObject.AnyAsync(x => x.OrderId == id);
    }
    
    public async Task AddCommentToOrder(int orderId, string comment)
    {
        var order = await _context.OrderObject.FindAsync(orderId);
        if (order != null)
        {
            order.OrderComment = comment;
            await _context.SaveChangesAsync();
        }
    }

public Task<List<OrderItem>> GetOrderObjectById(int orderId)
{
    try
    {
        return _context.OrderItemObject
            .Where(item => item.OrderId == orderId)
            .ToListAsync();
    }
    catch (Exception e)
    {
        _logger.LogError(e, $"Error occurred while fetching Object for order {orderId}.");
        throw;
    }
}


    public async Task<decimal> GetOrderPrice(int id)
    { 
        var orderObject = await GetOrderObjectById(id);
        decimal orderPrice = orderObject.Sum(item => (decimal)item.ItemPrice);
        return orderPrice;
    }

}