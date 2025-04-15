using System.ComponentModel.DataAnnotations;

namespace apiPayment.Models
{
    public class PaymentRequest
    {
        public string? PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationDate { get; set; }
        public string? Cvv { get; set; }
        public string? Pixkey { get; set; }
    }
}
