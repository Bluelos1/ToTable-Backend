
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderService
{
    Task<List<Order>> GetOrderItems();
    Task<Order> GetOrder(int id);
    Task PostOrder(Order Order);
    Task PutOrder(int id, Order Order);
    Task DeleteOrder(int id);
}

