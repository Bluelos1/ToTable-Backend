using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Contract;
using ToTable.Controllers;
using ToTable.Interfaces;
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

    public Task<List<Table>> GetTableObject()
    {
        try
        {
            return  _context.TableObject.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Notfound");
            throw;
        }
    }

    public async Task<Table> GetTable(int id)
    {
        var table = await _context.TableObject.FirstOrDefaultAsync(x => x.TabId == id);
        return table;
    }

        public async Task PostTable(TableDto table)
        {
            var tableItem = new Table
            {
                TabNum = table.TabNum,
                TabStatus = table.TabStatus,
                RestaurantId = table.RestaurantId
            };
            _context.TableObject.Add(tableItem);
            await _context.SaveChangesAsync();
            table.RestaurantId = tableItem.RestaurantId;
        }

    public async Task PutTable(int id, TableDto table)
    {
        var tableItem = await _context.TableObject.FirstOrDefaultAsync(x => x.TabId == id);
        tableItem.TabNum = table.TabNum;
        tableItem.TabStatus = table.TabStatus;
        tableItem.RestaurantId = table.RestaurantId;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTable(int id)
    {
        var table = await _context.TableObject.FindAsync(id);
        if (table != null)
        {
            _context.TableObject.Remove(table);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> TableExists(int id)
    {
        return null;
    }
    
    public async Task<int> GetAvailableTableId()
    {
        var availableTable = await _context.TableObject
            .FirstOrDefaultAsync(t => t.TabStatus);
        return availableTable?.TabId ?? 0; 
    }

    public async Task<IEnumerable<Table>> GetTablesByRestaurantId(int restaurantId)
{
    return await _context.TableObject
        .Where(table => table.RestaurantId == restaurantId)
        .ToListAsync();
}

}