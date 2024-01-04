using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetOrderItems()
        {
          if (_context.PaymentItems == null)
          {
              return NotFound();
          }
            return await _context.PaymentItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetOrder(int id)
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
            return await paymentItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Payment payment)
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
        public async Task<ActionResult<Payment>> PostOrder(Payment payment)
        {
          if (_context.PaymentItems == null)
          {
              return Problem("Entity set 'ToTableDbContext.PaymentItems'  is null.");
          }
            _context.PaymentItems.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.PayId }, payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
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

            await _paymentService.DeletePayment(id);
            return NoContent();
        }
    }
}
