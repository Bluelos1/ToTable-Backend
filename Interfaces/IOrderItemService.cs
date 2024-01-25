using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderItemService
{
    Task<List<OrderItemDto>> GetOrderItemObject();
    Task<OrderItem> GetOrderItem(int id);
    Task PutOrderItem(int id, OrderItemDto OrderItem);
    Task DeleteOrderItem(int id);
    Task PostOrderItem(OrderItemDto orderItemDto);
    Task<List<OrderItem>> GetAllOrderObjectByOrderId(int orderId);

}