using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ToTableDbContext _context;

        public PaymentController(ToTableDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentItems()
        {
            if (_context.PaymentItems == null)
            {
                return NotFound();
            }

            return await _context.PaymentItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            if (_context.PaymentItems == null)
            {
                return NotFound();
            }

            var payment = await _context.PaymentItems.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.PayId)
            {
                return BadRequest("Invalid Payment ID");
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound("Payment not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            if (_context.PaymentItems == null)
            {
                return Problem("Entity set 'ToTableDbContext.PaymentItems' is null.");
            }

            _context.PaymentItems.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.PayId }, payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.PaymentItems == null)
            {
                return NotFound();
            }

            var payment = await _context.PaymentItems.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.PaymentItems.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.PaymentItems?.Any(e => e.PayId == id)).GetValueOrDefault();
        }
    }
}
