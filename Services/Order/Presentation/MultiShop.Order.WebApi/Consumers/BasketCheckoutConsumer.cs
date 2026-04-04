using MassTransit;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Shared.Events.Abstract;
using MultiShop.Shared.Events.Concrete;

namespace MultiShop.Order.WebApi.Consumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;

        public BasketCheckoutConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var basketData = context.Message;

            // 🚀 TRENDYOL MANTIĞI: Satıcıya göre paketleri ayır
            var packages = basketData.BasketItems.GroupBy(x => x.VendorId);

            foreach (var package in packages)
            {
                var vendorId = package.Key; // Satıcı (Örn: Apple Store)
                var itemsInPackage = package.ToList(); // O satıcının ürünleri

                // Her paket için ayrı bir "CreateOrderingCommand" fırlatıyoruz
                await _mediator.Send(new CreateOrderingCommand
                {
                    UserId = basketData.UserId,
                    VendorId = vendorId, // 🎯 KRİTİK: Her paket kendi satıcısını bilir!

                    // Sadece bu paketteki ürünlerin fiyat toplamı
                    TotalPrice = itemsInPackage.Sum(x => x.Price * x.Quantity),
                    SenderCustomer=$"MultiShop-{vendorId}", 
                    // Sadece bu paketteki ürünlerin fiziksel verileri (Kargo için)
                    Weight = itemsInPackage.Sum(x => x.Weight),
                    Width = itemsInPackage.Max(x => x.Width),
                    Height = itemsInPackage.Max(x => x.Height),
                    Length = itemsInPackage.Max(x => x.Length),
                    CargoCustomerId= 1,
                    

                    OrderDetails = itemsInPackage.Select(x => new CreateOrderDetailCommand
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        ProductPrice = x.Price,
                        ProductAmount = x.Quantity,
                        VendorId = x.VendorId,
                        
                    }).ToList(),

                    AddressId = int.TryParse(basketData.AddressId, out int id) ? id : 0,
                    CargoCompanyId = 3005 // PTT
                });
            }
        }
    }
}