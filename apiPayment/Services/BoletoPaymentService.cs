using apiPayment.methods;
using apiPayment.Models;
using apiPayment.Services.Interface;

namespace apiPayment.Services
{
    public class BoletoPaymentService : IPaymentService
    {
        public async Task<string> ProcessPayment(decimal amount, PaymentRequest request)
        {
            try
            {
                var boletoNumber = GenerateCode.GenerateBoletoCode();
                await Task.Delay(1000);
                return $"Payment of {amount} processed successfully with boleto. Code: {boletoNumber}";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
