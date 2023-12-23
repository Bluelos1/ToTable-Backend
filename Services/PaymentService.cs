using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ToTableDbContext _context;

        public PaymentService(ToTableDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetPaymentItems()
        {
            return await _context.PaymentItems.ToListAsync();
        }

        public async Task<Payment> GetPayment(int id)
        {
            return await _context.PaymentItems.FirstOrDefaultAsync(x => x.PayId == id);
        }

        public async Task PostPayment(Payment payment)
        {
            _context.PaymentItems.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task PutPayment(int id, Payment payment)
        {
            if (id != payment.PayId)
            {
           
                throw new ArgumentException("Invalid Payment ID");
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
              
                    throw new InvalidOperationException("Payment not found");
                }
                else
                {
              
                    throw;
                }
            }
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _context.PaymentItems.FindAsync(id);
            if (payment != null)
            {
                _context.PaymentItems.Remove(payment);
                await _context.SaveChangesAsync();
            }
            else
            {
       
                throw new InvalidOperationException("Payment not found");
            }
        }

        private bool PaymentExists(int id)
        {
            return _context.PaymentItems.Any(e => e.PayId == id);
        }
    }
}
