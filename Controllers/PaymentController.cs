using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
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
            var item = _paymentService.GetPaymentItems();
          if (item == null)
          {
              return NotFound();
          }
            return await item;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetOrder(int id)
        {
            var paymentItem = _paymentService.GetPayment(id);
            if (paymentItem == null)
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
                return BadRequest();
            }

            await _paymentService.PutPayment(id, payment);
            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<Payment>> PostOrder(Payment payment)
        {
            await _paymentService.PostPayment(payment);
            return CreatedAtAction("GetOrder", new { id = payment.PayId }, payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_paymentService.GetPayment(id) == null)
            {
                return NotFound();
            }

            await _paymentService.DeletePayment(id);
            return NoContent();
        }
    }
}
