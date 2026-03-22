using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Payment.Dtos;
using MultiShop.Payment.Services.Abstract;

namespace MultiShop.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("CreatePaymentForm")]
        public async Task<IActionResult> CreatePaymentForm(PaymentRequestDTO requestDto)
        {
            // Servisimiz SOLID sayesinde sadece işini yapıyor
            var result = await _paymentService.CheckoutFormInitializeAsync(requestDto);

            if (result.Status == "success")
            {
                // result.CheckoutFormContent: iyzico'nun basacağı HTML/JS bloğu
                return Ok(new
                {
                    content = result.CheckoutFormContent,
                    token = result.Token
                });
            }

            return BadRequest(new { message = result.ErrorMessage });
        }

        // iyzico ödeme bittiğinde buraya POST atar. 
        // iyzico veriyi JSON değil "application/x-www-form-urlencoded" olarak gönderir.
        [HttpPost("Callback")]
        public async Task<IActionResult> Callback([FromForm] string token)
        {
            var result = await _paymentService.GetPaymentResultAsync(token);

            if (result.Status == "success" && result.PaymentStatus == "SUCCESS")
            {
                // BURASI ZAFER ANI: Para hesaba geçti!
                // Burada Sipariş Servisine (Order Microservice) "Ödeme Alındı" mesajı atabilirsin.
                return Ok(new { message = "Ödeme Başarıyla Tamamlandı!", paymentId = result.PaymentId });
            }

            return BadRequest(new { message = "Ödeme başarısız veya reddedildi.", error = result.ErrorMessage });
        }
    }
}
