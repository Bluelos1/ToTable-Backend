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
    private readonly ILogger<OrderItemService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "CartId";

    public OrderItemService(ToTableDbContext context, ILogger<OrderItemService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<OrderItem>> GetOrderItemObject()
    {
        try
        {
            return _context.OrderItemObject.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Not Found");
            throw;
        }
    }

    public Task<OrderItem> GetOrderItem(int id)
    {
        return _context.OrderItemObject.FirstOrDefaultAsync(x => x.ItemId == id);
    }   

    public async Task PostOrderItem(OrderItem OrderItem)
    {
        _context.OrderItemObject.Add(OrderItem);
        await _context.SaveChangesAsync();
    }

    public async Task PutOrderItem(int id, OrderItemDto OrderItem)
    {
        _context.Entry(OrderItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItem(int id)
    {
        var item = await _context.OrderItemObject.FindAsync(id);
        if (item != null)
        {
            _context.OrderItemObject.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task AddProductToOrder(OrderItemDto orderItemDto)
    {
        var product = await _context.ProductObject.FindAsync(orderItemDto.ProductId);
        var order = await _context.OrderObject.FirstOrDefaultAsync(x => x.OrderId== orderItemDto.ItemId); 
        
            
        var orderItem = new OrderItem()
        {
            OrderId = order.OrderId,
            ProductId = product.ProductId,
            ItemQuantity = orderItemDto.ItemQuantity,
            ItemPrice = product.ProductPrice * orderItemDto.ItemQuantity,
        };
        _context.OrderItemObject.Add(orderItem);
        await _context.SaveChangesAsync();
    }
    
    

    public async Task<List<OrderItem>> GetAllOrderObjectByOrderId(int orderId)
    {
        var orderObject = await _context
            .OrderItemObject.Where(x=>x.OrderId == orderId)
            .Include(x => x.Product)
            .ToListAsync();
        return orderObject;
    }

    public Task<bool> OrderItemExists(int id)
    {
        return _context.OrderItemObject.AnyAsync(x => x.ItemId == id);
    }
}