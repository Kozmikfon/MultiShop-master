using Iyzipay.Model;
using MultiShop.Payment.Dtos;

namespace MultiShop.Payment.Services.Abstract
{
    public interface IPaymentService
    {
        Task<CheckoutFormInitialize> CheckoutFormInitializeAsync(PaymentRequestDTO paymentRequestDTO);
        Task<CheckoutForm> GetPaymentResultAsync(string token);
    }
}
