using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Options;
using MultiShop.Payment.Dtos;
using MultiShop.Payment.Services.Abstract;
using MultiShop.Payment.Settings;
// Alias tanımlayarak çakışmayı önleyelim
using IyzicoBasketItem = Iyzipay.Model.BasketItem;

namespace MultiShop.Payment.Services.Concrete
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly Iyzipay.Options _iyzipayOptions;

        public IyzicoPaymentService(IOptions<IyzicoSettings> settings)
        {
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
            var result = await Task.Run(() => CheckoutFormInitialize.Create(request, _iyzipayOptions));
            return result;
        }

        public async Task<CheckoutForm> GetPaymentResultAsync(string token)
        {
            // Token ile iyzico'ya sorgu atıyoruz
            var request = new RetrieveCheckoutFormRequest { Token = token };

            // Senkron metodu asenkron sarmalıyoruz (Iyzico SDK'sı senkrondur)
            var result = await Task.Run(() => CheckoutForm.Retrieve(request, _iyzipayOptions));

            return result;
        }

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
                CallbackUrl = dto.CallbackUrl,

                Buyer = new Buyer
                {
                    Id = dto.BuyerId,
                    Name = dto.Name,
                    Surname = dto.Surname,
                    GsmNumber = dto.GsmNumber,
                    Email = dto.Email,
                    IdentityNumber = dto.IdentityNumber,
                    RegistrationAddress = dto.RegistrationAddress,
                    Ip = dto.IpAddress ?? "85.100.1.1",
                    City = dto.City,
                    Country = "Turkey" ,
                    ZipCode =dto.ZipCode ?? "34000",
                },

                BillingAddress = new Address
                {
                    ContactName = $"{dto.Name} {dto.Surname}",
                    City = dto.City,
                    Country = "Turkey",
                    Description = dto.RegistrationAddress,
                    ZipCode = dto.ZipCode ?? "34000"
                }
            };

            request.ShippingAddress = request.BillingAddress;

            // --- DİNAMİK SEPET İŞLEME ---
            // --- DİNAMİK SEPET İŞLEME ---
            // --- DİNAMİK SEPET İŞLEME ---
            var basketItems = new List<IyzicoBasketItem>();

            if (dto.BasketItems != null && dto.BasketItems.Any())
            {
                foreach (var item in dto.BasketItems)
                {
                    // Eğer Quantity gönderilmemişse 1 kabul et
                    var quantity = item.Quantity <= 0 ? 1 : item.Quantity;

                    basketItems.Add(new IyzicoBasketItem
                    {
                        Id = item.ProductId ?? Guid.NewGuid().ToString(),
                        Name = item.ProductName ?? "Ürün",
                        Category1 = item.Category1 ?? "General",
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        // Birim fiyatı iyzico'ya gönderiyoruz
                        Price = item.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            }
            else
            {
                basketItems.Add(new IyzicoBasketItem
                {
                    Id = "DEFAULT",
                    Name = "MultiShop Ürün Grubu",
                    Category1 = "Genel",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = dto.Price.ToString().Replace(",", ".")
                });
            }

            request.BasketItems = basketItems;
            return request;
        }
    }
}