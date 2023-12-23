using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;
using ToTable.Services;

namespace ToTable.Controllers
{
    [Route("api/waiters")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        private readonly IWaiterService _waiterService;

        public WaiterController(IWaiterService waiterService)
        {
            _waiterService = waiterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Waiter>>> GetWaiters()
        {
            var waiters = await _waiterService.GetWaitersAsync();
            return waiters;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Waiter>> GetWaiterById(int id)
        {
            var waiter = await _waiterService.GetWaiterByIdAsync(id);

            if (waiter == null)
            {
                return NotFound();
            }

            return waiter;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddWaiter(Waiter waiter)
        {
            var id = await _waiterService.AddWaiterAsync(waiter);
            return id;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateWaiter(int id, Waiter waiter)
        {
            if (id != waiter.WaiterId)
            {
                return BadRequest();
            }

            var result = await _waiterService.UpdateWaiterAsync(waiter);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteWaiter(int id)
        {
            var result = await _waiterService.DeleteWaiterAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return result;
        }
    }
}
