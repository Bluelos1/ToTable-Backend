using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderService
{
    Task<List<Order>> GetOrderItems();
    Task<Order> GetOrder(int id);
    Task<int> PostOrder(Order Order);
    Task PutOrder(int id, Order Order);
    Task DeleteOrder(int id);
    Task AddCommentToOrder(int orderId, string comment);
    Task<bool> OrderExists(int id);
    Task<decimal> GetOrderPrice(int id);
}

