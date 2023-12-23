
using ToTable.Models;

namespace ToTable.Interfaces;


public interface IPaymentService
{
    Task<List<Payment>> GetPaymentItems();
    Task<Payment> GetPayment(int id);
    Task PostPayment(Payment payment);
    Task PutPayment(int id, Payment payment);
    Task DeletePayment(int id);
}