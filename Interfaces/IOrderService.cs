using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderService
{
    Task<List<Order>> GetOrderItems();
    Task<Order> GetOrder(int id);
    Task PostOrder(Order Order);
    Task PutOrder(int id, Order Order);
    Task DeleteOrder(int id);
    Task AddCommentToOrder(int orderId, string comment);
    Task<bool> OrderExists(int id);
    Task<List<OrderItem>> GetOrderItemsById(int orderId);
    Task<decimal> GetOrderPrice(int id);
}

