using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Controllers
{
    public interface ITableService
    {
        Task<List<Table>> GetTableItems();
        Task<Table> GetTable(int id);
        Task PostTable(Table table);
        Task PutTable(int id, Table table);
        Task DeleteTable(int id);
    }
}
