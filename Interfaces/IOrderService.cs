using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrderItems();
        Task<Order> GetOrder(int id);
        Task PostOrder(Order order);
        Task PutOrder(int id, Order order);
        Task DeleteOrder(int id);
    }
}
