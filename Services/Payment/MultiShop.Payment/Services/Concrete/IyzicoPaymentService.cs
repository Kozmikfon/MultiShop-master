using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using MultiShop.Payment.Dtos;
using MultiShop.Payment.Services.Abstract;
using MultiShop.Payment.Settings;

namespace MultiShop.Payment.Services.Concrete
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly Iyzipay.Options _iyzipayOptions;

        public IyzicoPaymentService(IOptions<IyzicoSettings> settings)
        {
            // SOLID: Bağımlılığı dışarıdan (Options Pattern) alıyoruz
            _iyzipayOptions = new Iyzipay.Options
            {
                ApiKey = settings.Value.ApiKey,
                SecretKey = settings.Value.SecretKey,
                BaseUrl = settings.Value.BaseUrl
            };
        }

        public async Task<CheckoutFormInitialize> CheckoutFormInitializeAsync(PaymentRequestDTO dto)
        {
            var request = CreateBaseRequest(dto);

            // iyzico SDK'sı üzerinden formu oluşturuyoruz
            var result = await Task.Run(() => CheckoutFormInitialize.Create(request, _iyzipayOptions));
            return result;
        }

        public async Task<CheckoutForm> GetPaymentResultAsync(string token)
        {
            var request = new RetrieveCheckoutFormRequest { Token = token };
            var result = await Task.Run(() => CheckoutForm.Retrieve(request, _iyzipayOptions));
            return result;
        }

        // Helper Method: SOLID (Private metodlarla sorumluluğu bölüyoruz)
        private CreateCheckoutFormInitializeRequest CreateBaseRequest(PaymentRequestDTO dto)
        {
            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = dto.Price.ToString().Replace(",", "."),
                PaidPrice = dto.PaidPrice.ToString().Replace(",", "."),
                Currency = Currency.TRY.ToString(),
                BasketId = dto.BasketId,
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://localhost:7076/api/Payments/Callback", // Senin Callback URL'in

                Buyer = new Buyer
                {
                    Id = dto.BuyerId,
                    Name = dto.Name,
                    Surname = dto.Surname,
                    GsmNumber = dto.GsmNumber,
                    Email = dto.Email,
                    IdentityNumber = "11111111111", // Test TCKN
                    RegistrationAddress = dto.RegistrationAddress,
                    Ip = "85.100.1.1",
                    City = dto.City,
                    Country = "Turkey",
                    ZipCode = "58000"
                },

                BillingAddress = new Address
                {
                    ContactName = $"{dto.Name} {dto.Surname}",
                    City = dto.City,
                    Country = "Turkey",
                    Description = string.IsNullOrEmpty(dto.RegistrationAddress) ? "Adres Bilgisi Girilmedi" : dto.RegistrationAddress,
                    ZipCode = "58000"
                },

                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Id = "B101",
                        Name = "MultiShop Order",
                        Category1 = "General",
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        Price = dto.Price.ToString().Replace(",", ".")
                    }
                }
            };
            request.ShippingAddress = request.BillingAddress;
            return request;
        }
    }
}