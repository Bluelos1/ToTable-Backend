using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Controllers;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services
{
    public class TableService : ITableService
    {
        private readonly ToTableDbContext _context;

        public TableService(ToTableDbContext context)
        {
            _context = context;
        }
        private bool TableExist(int id)
{
    return _context.TableItems.Any(e => e.TabId == id);
}


        public async Task<List<Table>> GetTableItems()
        {
            return await _context.TableItems.ToListAsync();
        }

        public async Task<Table> GetTable(int id)
        {
            return await _context.TableItems.FirstOrDefaultAsync(x => x.TabId == id);
        }

        public async Task PostTable(Table table)
        {
            _context.TableItems.Add(table);
            await _context.SaveChangesAsync();
        }

        public async Task PutTable(int id, Table table)
        {
            if (id != table.TabId)
            {
                // Błąd zgodności ID
                throw new System.ArgumentException("Invalid Table ID");
            }

            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExist(id))
                {
                    
                    throw new System.InvalidOperationException("Table not found");
                }
                else
                {
               
                    throw;
                }
            }
        }

        public async Task DeleteTable(int id)
        {
            var table = await _context.TableItems.FindAsync(id);
            if (table != null)
            {
                _context.TableItems.Remove(table);
                await _context.SaveChangesAsync();
            }
            else
            
                throw new System.InvalidOperationException("Table not found");
            }
        }

      
    }

