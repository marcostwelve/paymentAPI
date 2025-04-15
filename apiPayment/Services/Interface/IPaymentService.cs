using apiPayment.Models;

namespace apiPayment.Services.Interface
{
    public interface IPaymentService
    {
        Task<string> ProcessPayment(decimal amount, PaymentRequest request);
    }
}
