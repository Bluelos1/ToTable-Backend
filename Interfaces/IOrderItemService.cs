using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderItemService
{
    Task<List<OrderItem>> GetOrderItemItems();
    Task<OrderItem> GetOrderItem(int id);
    Task PostOrderItem(OrderItem OrderItem);
    Task PutOrderItem(int id, OrderItem OrderItem);
    Task DeleteOrderItem(int id);
    Task AddProductToOrder(OrderItemDto orderItemDto);
    

}