using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class OrderItemService : IOrderItemService
{
    
    private readonly ToTableDbContext _context;
    private readonly TableService _tableService;
    private readonly WaiterService _waiterService;
    private readonly ILogger<OrderItemService> _logger;


    public OrderItemService(ToTableDbContext context, ILogger<OrderItemService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<OrderItem>> GetOrderItemItems()
    {
        try
        {
            return _context.OrderItemItems.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Not Found");
            throw;
        }
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

    public async Task PutOrderItem(int id, OrderItem OrderItem)
    {
        _context.Entry(OrderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItem(int id)
    {
        var item = await _context.OrderItemItems.FindAsync(id);
        if (item != null)
        {
            _context.OrderItemItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task AddProductToOrder(OrderItemDto orderItemDto)
    {
        var product = await _context.ProductItems.FindAsync(orderItemDto.ProductId);
        var order = await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId == orderItemDto.OrderItemId);
        if (order != null)
        {
            new Order
            {
                OrderTime = DateTime.Now,
                OrderStatus = OrderStatus.New,
                OrderComment = null,
                WaiterId = await _waiterService.GetAvailableWaiterId(),
                TableId = await _tableService.GetAvailableTableId(),
                PaymentId = 0,
            };
            _context.OrderItems.Add(order);
            await _context.SaveChangesAsync(); 
        }
        
        var orderItem = new OrderItem
        {
            OrderId = order?.OrderId,
            ProductId = product.ProductId,
            ItemQuantity = orderItemDto.ItemQuantity,
            ItemPrice = product.ProductPrice * orderItemDto.ItemQuantity
        };
        _context.OrderItemItems.Add(orderItem);
        await _context.SaveChangesAsync();

    }

    public Task<bool> OrderItemExists(int id)
    {
        return _context.OrderItemItems.AnyAsync(x => x.ItemId == id);
    }

    public Task<List<OrderItem>> GetOrderItemsByOrderId(int orderId)
    {
        throw new NotImplementedException();
    }

}