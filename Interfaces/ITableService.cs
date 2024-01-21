using Microsoft.AspNetCore.Mvc;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Controllers;

public interface ITableService
{
    Task<List<Table>> GetTableObject();
    Task<Table> GetTable(int id);
    Task PostTable(TableDto table);
    Task PutTable(int id, TableDto table);
    Task DeleteTable(int id);
    Task<int> GetAvailableTableId();
}