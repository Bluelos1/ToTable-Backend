using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Interfaces
{
    public interface IOrderItemItemService
    {
        Task<List<OrderItem>> GetOrderItemItems();
        Task<OrderItem> GetOrderItem(int id);
        Task PostOrderItem(OrderItem orderItem);
        Task PutOrderItem(int id, OrderItem orderItem);
        Task DeleteOrderItem(int id);
    }
}
