using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;
namespace ToTable.Services
{
    public interface IWaiterService
    {
        Task<List<Waiter>> GetWaitersAsync();
        Task<Waiter> GetWaiterByIdAsync(int waiterId);
        Task<int> AddWaiterAsync(Waiter waiter);
        Task<bool> UpdateWaiterAsync(Waiter waiter);
        Task<bool> DeleteWaiterAsync(int waiterId);
    }
}
