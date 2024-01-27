using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
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
            var item = await _tableService.GetTableObject();
            if (item== null || !item.Any())
            {
                return NotFound();
            }
            return  Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = _tableService.GetTable(id);
            
            if (table == null)
            {
                return NotFound();
            }

            return  Ok(table);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, TableDto table, IValidator<TableDto> validator)
        {
            var validationResult = await validator.ValidateAsync(table);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (id != table.TabId)
            {
                return BadRequest("Mismatch between ID in URL and table ID.");
            }

            try
            {
                await _tableService.PutTable(id, table);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the table.");
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Table>> PostTable(TableDto table,IValidator<TableDto> validator)
        {
            var validationResult = await validator.ValidateAsync(table);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            try
            {
                await _tableService.PostTable(table);
                return CreatedAtAction("GetTable", new { id = table.TabId }, table);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
