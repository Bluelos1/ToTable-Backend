using Microsoft.EntityFrameworkCore;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class PaymentService : IPaymentService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ToTableDbContext context, ILogger<PaymentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Payment>> GetPaymentItems()
    {
        try
        {
            return await _context.PaymentItems.ToListAsync();
        }
        catch (Exception e)
        {
            
            _logger.LogError(e,"NotFound");
            throw;
        }
    }

    public async Task<Payment?> GetPayment(int id)
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
        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeletePayment(int id)
    {
        var item = await _context.PaymentItems.FindAsync(id);
        if (item != null)
        {
            _context.PaymentItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> PaymentExists(int id)
    {
        return _context.PaymentItems.AnyAsync(x => x.PayId == id);
    } 
}