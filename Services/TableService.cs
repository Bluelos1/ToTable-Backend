using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Controllers;
using ToTable.Models;

namespace ToTable.Services;

public class TableService : ITableService
{
    private readonly ToTableDbContext _context;

    public TableService(ToTableDbContext context)
    {
        _context = context;
    }

    public Task<List<Table>> GetTableItems()
    {
       return  _context.TableItems.ToListAsync();
    }

    public Task<Table> GetTable(int id)
    {
        var table =_context.TableItems.FirstOrDefaultAsync(x => x.TabId == id);
        return table;
    }

    public async Task PostTable(Table table)
    {
         _context.TableItems.Add(table);
         await _context.SaveChangesAsync();
    }

    public async Task PutTable(int id, Table table)
    {
        _context.Entry(table).State = EntityState.Modified;
    }

    public async Task DeleteTable(int id)
    {
        var table = await _context.TableItems.FindAsync(id);
        if (table != null)
        {
            _context.TableItems.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
    
}