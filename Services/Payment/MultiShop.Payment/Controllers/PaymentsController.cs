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
        private readonly IOrderService _orderService;

        public PaymentsController(IPaymentService paymentService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }
        [HttpPost("CreatePaymentForm")]
        public async Task<IActionResult> CreatePaymentForm(PaymentRequestDTO requestDto)
        {
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

        /// <summary>
        /// iyzico ödeme işlemi bittiğinde (başarılı veya başarısız) buraya POST atar.
        /// </summary>
        [HttpPost("Callback")]
        public async Task<IActionResult> Callback([FromForm] string token)
        {
            // 1. Güvenlik ve Null Kontrolü
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Token Bulunamadı");
            }

            // 2. iyzico'dan ödeme sonucunu doğrula (Token ile sorgulama)
            var result = await _paymentService.GetPaymentResultAsync(token);

            // 3. Ödeme Başarılı mı? (Hem genel status 'success' olmalı hem de ödeme durumu 'SUCCESS')
            if (result.Status == "success" && result.PaymentStatus == "SUCCESS")
            {
                // 4. Sipariş mikroservisini güncelle (Ordering.Api'ye istek atar)
                // BasketId: Senin ödemeyi başlatırken gönderdiğin sipariş ID'sidir.
                var isOrderUpdated = await _orderService.UpdateOrderPaymentStatusAsync(result.BasketId, true);

                if (isOrderUpdated)
                {
                    // Ödeme başarılı ve sipariş güncellendi.
                    // Not: Gerçek hayatta burada bir Redirect (Yönlendirme) yaparak 
                    // kullanıcıyı frontend'deki "Başarılı" sayfasına göndermelisin.
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Ödeme Alındı, Sipariş Onaylandı!",
                        BasketId = result.BasketId,
                        PaymentId = result.PaymentId // iyzico'nun verdiği işlem ID'si
                    });
                }
                else
                {
                    // Ödeme çekildi ama sipariş servisi (Ordering) güncellenemedi.
                    // CRITICAL: Bu durum loglanmalı. Telafi (Retry) mekanizması gerekebilir.
                    return StatusCode(500, new
                    {
                        Status = "TechnicalError",
                        Message = "Ödeme başarıyla alındı fakat sipariş durumu güncellenemedi.",
                        BasketId = result.BasketId,
                        PaymentId = result.PaymentId
                    });
                }
            }

            // 5. Ödeme Başarısız Durumu
            // Kullanıcı kart limit yetersizliği, yanlış şifre vb. bir hata almış olabilir.
            return BadRequest(new
            {
                Status = "PaymentFailed",
                Message = "Ödeme işlemi başarısız.",
                Error = result.ErrorMessage, // iyzico'nun kullanıcıya gösterilecek hata mesajı
                ErrorCode = result.ErrorCode
            });
        }
    }
}
