using apiPayment.Models;
using apiPayment.Services.Interface;

namespace apiPayment.Services
{
    public class PixPaymentService : IPaymentService
    {
        public async Task<string> ProcessPayment(decimal amount, PaymentRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Pixkey))
                    return "Error! Pix key is required";


                await Task.Delay(1000);

                return $"Payment of {amount} processed successfully with Pix key {request.Pixkey}.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
