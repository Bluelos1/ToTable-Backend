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
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Table>>> GetTableItems([FromServices] ITableService _tableService)
        {
          return await _tableService.GetTableItems();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id,[FromServices] ITableService _tableService)
        {
            var table = _tableService.GetTable(id);
            
            if (table == null)
            {
                return NotFound();
            }

            return await table;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table, [FromServices] ITableService _tableService)
        {
            if (id != table.TabId)
            {
                return BadRequest();
            }

            try
            {
                await _tableService.PutTable(id,table);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_tableService.GetTable(id).IsCanceled)
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
        public async Task<ActionResult<Table>> PostTable(Table table, [FromServices] ITableService _tableService)
        {
            await _tableService.PostTable(table);
            return CreatedAtAction("GetTable", new { id = table.TabId }, table);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id, [FromServices] ITableService _tableService)
        {
           await _tableService.DeleteTable(id);
           return NoContent();
        }
    }
}
