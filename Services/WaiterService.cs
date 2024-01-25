using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
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


    public async Task PostWaiter(WaiterDto waiter)
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
        waiter.WaiterId = waiterItem.WaiterId;
    }

    public async Task PutWaiter(int id, WaiterDto waiter)
    {
        var waitreItem = await _context.WaiterObject.FirstOrDefaultAsync(x => x.WaiterId == id);
        waitreItem.WaiterName = waiter.WaiterName;
        waitreItem.WaiterSurname = waiter.WaiterSurname;
        waitreItem.WaiterLogin = waiter.WaiterLogin;
        waitreItem.WaiterPassw = waiter.WaiterPassw;
        waitreItem.IsAvailable = waiter.IsAvailable;
        waitreItem.RestaurantId = waiter.RestaurantId;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWaiter(int id)
    {
        var waiter = await _context.WaiterObject.Include("Orders").FirstOrDefaultAsync(x => x.WaiterId == id);
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
    

}