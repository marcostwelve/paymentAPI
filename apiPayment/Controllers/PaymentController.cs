using apiPayment.Models;
using apiPayment.Services;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace apiPayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentProcessorService _paymentProcessorService;

        public PaymentController()
        {
            _paymentProcessorService = new PaymentProcessorService();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            try
            {
                var result = await _paymentProcessorService.ProcessPayment(request);

                if (result.StartsWith("Error"))
                    return BadRequest(new { message = result });

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("pix")]
        public async Task<IActionResult> ProcessPixPaymentRequest([FromBody] PaymentRequest request)
        {
            try
            {
                if (request.PaymentType == "Pix")
                {
                    string pixCode = GeneratePixCode(request.Amount);
                    byte[] qrCodeImage = GenerateQRCode(pixCode);

                    var base64qrCode = Convert.ToBase64String(qrCodeImage);
                    return Ok(new {pixCode, qrCode = base64qrCode});
                    // return File(qrCodeImage, "image/png");

                }
                else
                {
                    return BadRequest(new { message = "Error! Invalid payment type" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private string GeneratePixCode(decimal amount)
        {
            return  $"00020126330014BR.GOV.BCB.PIX0114+55119999999955204000053039865406{amount}5802BR5920Nome da Empresa6008SaoPaulo62140514QRCODEPIX12345663041D3F";
        }

        private byte[] GenerateQRCode(string pixCode)
        {
            using (QRCodeGenerator qrCodeGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(pixCode, QRCodeGenerator.ECCLevel.Q))
           
            {
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                return qrCode.GetGraphic(20);
            }
        }
    }
}
