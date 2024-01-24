using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
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
        public async Task<ActionResult<List<Table>>> GetTableObject()
        {
          return await _tableService.GetTableObject();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = _tableService.GetTable(id);
            
            if (table == null)
            {
                return NotFound();
            }

            return await table;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, TableDto table)
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
        public async Task<ActionResult<Table>> PostTable(TableDto table, [FromServices] ITableService _tableService)
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

         [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Table>>> GetTablesByRestaurantId(int restaurantId)
        {
            var tables = await _tableService.GetTablesByRestaurantId(restaurantId);
            if (tables == null)
            {
                return NotFound();
            }

            return Ok(tables);
        }


    }
}
