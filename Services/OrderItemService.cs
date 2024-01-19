using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToTable.Controllers;


namespace ToTable.Services;


public class OrderItemService : IOrderItemService
{
    
    private readonly ToTableDbContext _context;
    private readonly ITableService _tableService;
    private readonly IWaiterService _waiterService;
    private readonly ILogger<OrderItemService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "CartId";

    public OrderItemService(ToTableDbContext context, ITableService tableService, IWaiterService waiterService, ILogger<OrderItemService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _tableService = tableService;
        _waiterService = waiterService;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task PostOrderItem(OrderItem OrderItem)
    {
        _context.OrderItemItems.Add(OrderItem);
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
    
    public async Task<int> AddProductToOrder(OrderItemDto orderItemDto)
    {
        var product = await _context.ProductItems.FindAsync(orderItemDto.ProductId);
        var order = await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId== orderItemDto.OrderItemId); 
        var waiterId = await _waiterService.GetAvailableWaiterId();
        var tableId = await _tableService.GetAvailableTableId();
           
        order ??= new Order
            {
                OrderTime = DateTime.Now,
                OrderStatus = OrderStatus.New,
                OrderComment = null,
                WaiterId = waiterId,
                TableId = tableId,
                PaymentId = 0,
            }; 
        var waiter = await _context.WaiterItems.FindAsync(waiterId);
            if (waiter != null)
            {
                waiter.IsAvailable = false;
            } 
            var table = await _context.TableItems.FindAsync(tableId);
            if (table != null)
            {
                table.TabStatus = false;
            }
            _context.OrderItems.Add(order);
            await _context.SaveChangesAsync();
            
        var orderItem = new OrderItem
        {
            OrderId = order.OrderId,
            ProductId = product.ProductId,
            ItemQuantity = orderItemDto.ItemQuantity,
            ItemPrice = product.ProductPrice * orderItemDto.ItemQuantity,
            Product = product
        };
        _context.OrderItemItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return order.OrderId;
    }
    
    

    public async Task<List<OrderItem>> GetAllOrderItemsByOrderId(int orderId)
    {
        var orderitems = await _context
            .OrderItemItems.Where(x=>x.OrderId == orderId)
            .Include(x => x.Product)
            .ToListAsync();
        return orderitems;
    }

    public Task<bool> OrderItemExists(int id)
    {
        return _context.OrderItemItems.AnyAsync(x => x.ItemId == id);
    }
}