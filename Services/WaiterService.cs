using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class WaiterService : IWaiterService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<PaymentService> _logger;

    public WaiterService(ToTableDbContext context, ILogger<PaymentService> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<int> GetAvailableWaiterId()
    {
        var availableWaiter = await _context.WaiterItems
            .FirstOrDefaultAsync(w => w.IsAvailable);
        return availableWaiter?.WaiterId ?? 0;
    }

}