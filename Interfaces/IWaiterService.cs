using ToTable.Models;

namespace ToTable.Interfaces;


    public interface IWaiterService
    {
        Task<List<Waiter>> GetWaiterItems();
        Task<Waiter> GetWaiter(int id);
        Task PostWaiter(Waiter waiter);
        Task PutWaiter(int id, Waiter waiter);
        Task DeleteWaiter(int id);
        Task<int> GetAvailableWaiterId();
    }
