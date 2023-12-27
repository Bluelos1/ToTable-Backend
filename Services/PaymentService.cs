using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class PaymentService : IPaymentService
{
    private readonly ToTableDbContext _context;

    public PaymentService(ToTableDbContext context)
    {
        _context = context;
    }

    public Task<List<Payment>> GetPaymentItems()
    {
        throw new NotImplementedException();
    }

    public Task<Payment> GetPayment(int id)
    {
        throw new NotImplementedException();
    }

    public Task PostPayment(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task PutPayment(int id, Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task DeletePayment(int id)
    {
        throw new NotImplementedException();
    }
}