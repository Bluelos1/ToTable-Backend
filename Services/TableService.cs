using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Controllers;
using ToTable.Models;

namespace ToTable.Services;

public class TableService : ITableService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<TableService> _logger;


    public TableService(ToTableDbContext context, ILogger<TableService> logger)
    {
        _logger = logger;
        _context = context;
    }

    public Task<List<Table>> GetTableItems()
    {
        try
        {
            return  _context.TableItems.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Notfound");
            throw;
        }
    }

    public async Task<Table> GetTable(int id)
    {
        var table = await _context.TableItems.FirstOrDefaultAsync(x => x.TabId == id);
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
        await _context.SaveChangesAsync();
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

    public Task<bool> TableExists(int id)
    {
        return _context.PaymentItems.AnyAsync(x => x.PayId == id);
    }
}