using apiPayment.Models;
using apiPayment.Services.Interface;
using apiPayment.Utils;

namespace apiPayment.Services
{
    public class CreditCardPaymentService : IPaymentService
    {
        public async Task<string> ProcessPayment(decimal amount, PaymentRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.CardNumber))
                    return "Error! Card number is required";

                if (!CreditCardValidator.ValidateCreditCard(request.CardNumber))
                    return "Error! Invalid credit card number";

                string? cardType = CreditCardValidator.GetCardType(request.CardNumber);

                if (cardType == null)
                    return "Error! Invalid Flag card not supported";

                if (!CreditCardValidator.IsValidExpirationDate(request.ExpirationDate))
                    return "Error! Invalid expiration date";

                if (!CreditCardValidator.IsValidCvv(request.Cvv, cardType))
                    return "Error! Invalid CVV";

                await Task.Delay(1000);

                return $"Payment of {amount} processed successfully with credit card {cardType} {request.CardNumber[..4]}****.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
