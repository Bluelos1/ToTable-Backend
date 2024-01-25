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

    public OrderItemService(ToTableDbContext context, ILogger<OrderItemService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<OrderItemDto>> GetOrderItemObject()
    {
        try
        {
            return _context.OrderItemObject.Select(x => new OrderItemDto
            {
                ItemId = x.ItemId,
                ItemQuantity = x.ItemQuantity,
                ProductId = x.ProductId,
                OrderId = x.OrderId
            }).ToListAsync();
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

    // public async Task PostOrderItem(OrderItemDto orderItem)
    // {
    //     var item = new OrderItem
    //     {
    //         ProductId = orderItem.ProductId,
    //         ItemQuantity = orderItem.ItemQuantity,
    //         OrderId = orderItem.OrderId
    //     };
    //     _context.OrderItemObject.Add(item);
    //     await _context.SaveChangesAsync();
    //     orderItem.ItemId = item.ItemId;
    // }

    public async Task PutOrderItem(int id, OrderItemDto orderItem)
    {
        var item = await _context.OrderItemObject.FirstOrDefaultAsync(x => x.ItemId == id);
        item.OrderId = orderItem.OrderId;
        item.ProductId = orderItem.ProductId;
        item.ItemQuantity = orderItem.ItemQuantity;
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
    
 public async Task PostOrderItem(OrderItemDto orderItemDto)
{
    var order = await _context.OrderObject.FirstOrDefaultAsync(x => x.OrderId == orderItemDto.OrderId);
    var product = await _context.ProductObject.FindAsync(orderItemDto.ProductId);

    var existingOrderItem = await _context.OrderItemObject
        .FirstOrDefaultAsync(x => x.OrderId == orderItemDto.OrderId && x.ProductId == orderItemDto.ProductId);

    if (existingOrderItem != null)
    {
        existingOrderItem.ItemQuantity += orderItemDto.ItemQuantity;
        await _context.SaveChangesAsync();
    }
    else
    {
        var orderItem = new OrderItem()
        {
            OrderId = orderItemDto.OrderId,
            ProductId = orderItemDto.ProductId,
            ItemQuantity = orderItemDto.ItemQuantity,
        };
        _context.OrderItemObject.Add(orderItem);
        await _context.SaveChangesAsync();
        orderItemDto.ItemId = orderItem.ItemId;
    }
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

public async Task<OrderItem> UpdateOrderItemQuantity(int orderId, int itemId, int quantity)
    {
        var orderItem = await _context.OrderItemObject
            .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ItemId == itemId);

        if (orderItem == null)
        {
            throw new ArgumentException("The order item with the given order ID and item ID does not exist.");
        }

        orderItem.ItemQuantity = quantity;
        await _context.SaveChangesAsync();

        return orderItem;
    }
    
        public async Task DeleteOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        var orderItem = await _context.OrderItemObject
            .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
        if (orderItem != null)
        {
            _context.OrderItemObject.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
        
    }
}