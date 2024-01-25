using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;



public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersByRestaurantId(int restaurantId);
    Task<List<Order>> GetOrderObject();
    Task<Order> GetOrder(int id);
    Task<int> PostOrder(OrderDto Order);
    Task PutOrder(int id, OrderDto Order);
    Task DeleteOrder(int id);
    Task AddCommentToOrder(int orderId, string comment);
    Task<bool> OrderExists(int id);
}

