using MassTransit;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
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

            var orderDetails = basketData.BasketItems.Select(x => new CreateOrderDetailCommand
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductAmount = x.Quantity,
                ProductTotalPrice = x.Price * x.Quantity,
                VendorId = x.VendorId // Eğer basket'te varsa aktar
            }).ToList();


            // 🎯 SADECE TETİKLE: Tüm iş mantığını uzmanına (Handler) devrediyoruz.
            await _mediator.Send(new CreateOrderingCommand
            {
                UserId = basketData.UserId,
                TotalPrice = basketData.TotalPrice,
                AddressId = int.TryParse(basketData.AddressId, out int id) ? id : 0,

                OrderDetails=orderDetails,
                // Fiziksel veriler sepetten Handler'a akıyor
                Weight = basketData.TotalWeight,
                Width = basketData.BasketItems.Any() ? basketData.BasketItems.Max(x => x.Width) : 0,
                Height = basketData.BasketItems.Any() ? basketData.BasketItems.Max(x => x.Height) : 0,
                Length = basketData.BasketItems.Any() ? basketData.BasketItems.Max(x => x.Length) : 0,

                // Akıllı atama: Varsayılan kargo şirket ID'si
                CargoCompanyId = 3005,
                SenderCustomer = "MultiShop Ana Depo"
            });

            Console.WriteLine($">>>>> [CONSUMER]: BasketCheckoutEvent alındı ve Handler tetiklendi. <<<<<");
        }
    }
}