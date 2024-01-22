using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class WaiterService : IWaiterService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<WaiterService> _logger;

    public WaiterService(ToTableDbContext context, ILogger<WaiterService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public Task<List<Waiter>> GetWaiterObject()
    {
        return _context.WaiterObject.ToListAsync();   
    }




    public async Task<Waiter> GetWaiter(int id)
    {
        var waiter = await _context.WaiterObject.FirstOrDefaultAsync(x => x.WaiterId == id);
        return waiter;
    }


    public async Task PostWaiter(Waiter waiter)
    {
        var waiterItem = new Waiter
        {
            WaiterName = waiter.WaiterName,
            WaiterSurname = waiter.WaiterSurname,
            WaiterLogin = waiter.WaiterLogin,
            WaiterPassw = waiter.WaiterPassw,
            IsAvailable = true,
            RestaurantId = waiter.RestaurantId
        };

    _context.WaiterObject.Add(waiterItem);
        await _context.SaveChangesAsync();
    }

    public async Task PutWaiter(int id, Waiter waiter)
    {
        _context.Entry(waiter).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWaiter(int id)
    {
        var waiter = await _context.WaiterObject.FindAsync(id);
        if (waiter != null)
        {
            _context.WaiterObject.Remove(waiter);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<int> GetAvailableWaiterId()
    {
        var availableWaiter = await _context.WaiterObject
            .FirstOrDefaultAsync(w => w.IsAvailable);
        return availableWaiter?.WaiterId ?? 0;
    }
    
    public async Task<Waiter> GetWaiterByCredentials(string login, string password)
{
    return await _context.WaiterObject.FirstOrDefaultAsync(w => w.WaiterLogin == login && w.WaiterPassw == password);
}

}