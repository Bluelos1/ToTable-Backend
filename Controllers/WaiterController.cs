using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        private readonly ToTableDbContext _context;

        public WaiterController(ToTableDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetWaiterItems()
        {
            if (_context.WaiterItems == null)
            {
                return NotFound();
            }
            return await _context.WaiterItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
        {
            if (_context.WaiterItems == null)
            {
                return NotFound();
            }

            var waiterItem = await _context.WaiterItems.FindAsync(id);

            if (waiterItem == null)
            {
                return NotFound();
            }

            return Ok(waiterItem);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaiter(int id, Waiter waiter)
        {
            if (id != waiter.WaiterId)
            {
                return BadRequest();
            }

            _context.Entry(waiter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaiterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Waiter>> PostWaiter(Waiter waiter)
        {
            if (_context.WaiterItems == null)
            {
                return Problem("Entity set 'ToTableDbContext.WaiterItems'  is null.");
            }
            _context.WaiterItems.Add(waiter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaiter", new { id = waiter.WaiterId }, waiter);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            if (_context.WaiterItems == null)
            {
                return NotFound();
            }
            var waiter = await _context.WaiterItems.FindAsync(id);
            if (waiter == null)
            {
                return NotFound();
            }

            _context.WaiterItems.Remove(waiter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WaiterExists(int id)
        {
            return (_context.WaiterItems?.Any(e => e.WaiterId == id)).GetValueOrDefault();
        }
    }
}
