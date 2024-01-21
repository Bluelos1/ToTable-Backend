using Microsoft.AspNetCore.Mvc;
using ToTable.Models;

namespace ToTable.Controllers;

public interface ITableService
{
    Task<List<Table>> GetTableObject();
    Task<Table> GetTable(int id);
    Task PostTable(Table table);
    Task PutTable(int id, Table table);
    Task DeleteTable(int id);
    Task<int> GetAvailableTableId();
}