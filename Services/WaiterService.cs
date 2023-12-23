using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Services
{
    public class WaiterService : IWaiterService
    {
        private readonly ToTableDbContext _context;

        public WaiterService(ToTableDbContext context)
        {
            _context = context;
        }

        public async Task<List<Waiter>> GetWaitersAsync()
        {
            return await _context.WaiterItems.ToListAsync();
        }

        public async Task<Waiter> GetWaiterByIdAsync(int waiterId)
        {
            return await _context.WaiterItems.FindAsync(waiterId);
        }

        public async Task<int> AddWaiterAsync(Waiter waiter)
        {
            _context.WaiterItems.Add(waiter);
            await _context.SaveChangesAsync();
            return waiter.WaiterId;
        }

        public async Task<bool> UpdateWaiterAsync(Waiter waiter)
        {
            _context.Entry(waiter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWaiterAsync(int waiterId)
        {
            var waiter = await _context.WaiterItems.FindAsync(waiterId);
            if (waiter == null)
            {
                return false;
            }

            _context.WaiterItems.Remove(waiter);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
