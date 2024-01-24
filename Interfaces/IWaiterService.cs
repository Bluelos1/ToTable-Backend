using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;


    public interface IWaiterService
    {
        Task<List<Waiter>> GetWaiterObject();
        Task<Waiter> GetWaiter(int id);
        Task PostWaiter(WaiterDto waiter);
        Task PutWaiter(int id, WaiterDto waiter);
        Task DeleteWaiter(int id);
        Task<int> GetAvailableWaiterId();
        Task<Waiter> GetWaiterByCredentials(string login, string password);
        Task<IEnumerable<Waiter>> GetWaitersByRestaurantId(int restaurantId);
    }