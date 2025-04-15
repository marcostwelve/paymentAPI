using System.Text.RegularExpressions;

namespace apiPayment.Utils
{
    public class CreditCardValidator
    {
        ///<summary>
        /// This method validates a credit card number
        /// </summary>

        public static bool ValidateCreditCard(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || !Regex.IsMatch(cardNumber, @"^\d{13,19}$"))
                return false;


            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }
                sum += digit;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        ///<summary>
        /// Valida a bandeira do cartão de crédito com base nos primeiros dígitos.
        ///</summary>
        
        public static string? GetCardType(string cardNumber)
        {
            if (Regex.IsMatch(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$"))
                return "Visa";
            if (Regex.IsMatch(cardNumber, @"^5[1-5][0-9]{14}$"))
                return "MasterCard";
            if (Regex.IsMatch(cardNumber, @"^3[47][0-9]{13}$"))
                return "American Express";
            if (Regex.IsMatch(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$"))
                return "Discover";
            if (Regex.IsMatch(cardNumber, @"^(?:2131|1800|35\d{3})\d{11}$"))
                return "JCB";
            if (Regex.IsMatch(cardNumber, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"))
                return "Diners Club";

            return null;
        }

        /// <summary>
        /// Valida a data de validade do cartão (MM/YY).
        /// </summary>
        
        public static bool IsValidExpirationDate(string expirationDate)
        {
            if (!Regex.IsMatch(expirationDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                return false;

            var parts = expirationDate.Split('/');
            int month = int.Parse(parts[0]);
            int year = int.Parse(parts[1]) + 2000;

            var now = DateTime.UtcNow;

            var cardDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return cardDate >= now;
        }

        /// <summary>
        /// Valida o código de segurança (CVV).
        /// </summary>
        public static bool IsValidCvv(string cvv, string? cardType)
        {
            if (!Regex.IsMatch(cvv, @"^\d{3,4}$"))
                return false;

            return cardType == "American Express" ? cvv.Length == 4 : cvv.Length == 3;
        }
    }
}
