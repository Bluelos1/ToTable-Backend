using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        private readonly IWaiterService _waiterService;

        public WaiterController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetWaiter()
        {
            var waiter = await _waiterService.GetWaiterObject();
            if (waiter == null || !waiter.Any())
            {
                return NotFound();
            }
            return  Ok(waiter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiter(int id)
        {
            var waiter = await _waiterService.GetWaiter(id);
            if (waiter == null)
            {
                return NotFound();
            }
            return Ok(waiter);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaiter(int id, WaiterDto waiter, IValidator<WaiterDto> validator)
        {
            var validationResult = await validator.ValidateAsync(waiter);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (id != waiter.WaiterId)
            {
                return BadRequest();
            }

            try
            {
                await _waiterService.PutWaiter(id, waiter);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Waiter>> PostWaiter(WaiterDto waiter, IValidator<WaiterDto> validator)
        {
            var validationResult = await validator.ValidateAsync(waiter);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            try
            {
                await _waiterService.PostWaiter(waiter);
                return CreatedAtAction("GetWaiter", new { id = waiter.WaiterId }, waiter);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the waiter.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            var waiter = await _waiterService.GetWaiter(id);
            if (waiter == null)
            {
                return NotFound();
            }

            await _waiterService.DeleteWaiter(id);

            return NoContent();
        }

        [HttpGet("login/{login}/{password}")]
        public async Task<ActionResult<Waiter>> GetWaiterByCredentials(string login, string password)
        {
            var waiter = await _waiterService.GetWaiterByCredentials(login, password);
            if (waiter == null)
            {
                return NotFound();
            }
            return Ok(waiter);
        }

         [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Waiter>>> GetWaitersByRestaurantId(int restaurantId)
        {
            var waiters = await _waiterService.GetWaitersByRestaurantId(restaurantId);
            if (waiters == null)
            {
                return NotFound();
            }

            return Ok(waiters);
        }
        
    }
}