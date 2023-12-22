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
    public class TableController : ControllerBase
    {
        private readonly ToTableDbContext _context;

        public TableController(ToTableDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTableItems()
        {
          if (_context.TableItems == null)
          {
              return NotFound();
          }
            return await _context.TableItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
          if (_context.TableItems == null)
          {
              return NotFound();
          }
            var table = await _context.TableItems.FindAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            if (id != table.TabId)
            {
                return BadRequest();
            }

            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
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
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
          if (_context.TableItems == null)
          {
              return Problem("Entity set 'ToTableDbContext.TableItems'  is null.");
          }
            _context.TableItems.Add(table);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTable", new { id = table.TabId }, table);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            if (_context.TableItems == null)
            {
                return NotFound();
            }
            var table = await _context.TableItems.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _context.TableItems.Remove(table);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TableExists(int id)
        {
            return (_context.TableItems?.Any(e => e.TabId == id)).GetValueOrDefault();
        }
    }
}
