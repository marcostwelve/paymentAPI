using apiPayment.Models;
using apiPayment.Services.Interface;

namespace apiPayment.Services
{
    public class PaymentProcessorService
    {
        private readonly Dictionary<string, IPaymentService> _paymentMethods;

        public PaymentProcessorService()
        {
            _paymentMethods = new Dictionary<string, IPaymentService>
            {
                {"CreditCard", new CreditCardPaymentService() },
                { "Boleto", new BoletoPaymentService() },
                { "Pix", new PixPaymentService() }
            };
        }

        public async Task<string> ProcessPayment(PaymentRequest request)
        {
            if (!_paymentMethods.ContainsKey(request.PaymentType))
                return "Error! Invalid payment type";

            return await _paymentMethods[request.PaymentType].ProcessPayment(request.Amount, request);
        }
    }
}
