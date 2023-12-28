using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class WaiterService : IWaiterService
{
    private readonly ToTableDbContext _context;

    public WaiterService(ToTableDbContext context)
    {
        _context = context;
    }

    public Task<List<Waiter>> GetWaiterItems()
    {
        return _context.WaiterItems.ToListAsync();   
    }




    public async Task<Waiter> GetWaiter(int id)
    {
        var waiter = await _context.WaiterItems.FirstOrDefaultAsync(x => x.WaiterId == id);
        return waiter;
    }


    public async Task PostWaiter(Waiter waiter)
    {
        _context.WaiterItems.Add(waiter);
        await _context.SaveChangesAsync();
    }

    public async Task PutWaiter(int id, Waiter waiter)
    {
        _context.Entry(waiter).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWaiter(int id)
    {
        var waiter = await _context.WaiterItems.FindAsync(id);
        if (waiter != null)
        {
            _context.WaiterItems.Remove(waiter);
            await _context.SaveChangesAsync();
        }
    }
}