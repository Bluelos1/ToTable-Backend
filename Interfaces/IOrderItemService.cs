using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderItemService
{
    Task<List<OrderItem>> GetOrderItemObject();
    Task<OrderItem> GetOrderItem(int id);
    Task PostOrderItem(OrderItem OrderItem);
    Task PutOrderItem(int id, OrderItem OrderItem);
    Task DeleteOrderItem(int id);
    Task AddProductToOrder(OrderItemDto orderItemDto);
    Task<List<OrderItem>> GetAllOrderObjectByOrderId(int orderId);

}