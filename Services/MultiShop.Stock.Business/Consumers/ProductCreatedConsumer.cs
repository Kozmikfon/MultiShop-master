using MassTransit;
using MultiShop.Shared.Events.Concrete;
using MultiShop.Stock.BusinessLayer.Abstract;
using MultiShop.Stock.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.BusinessLayer.Consumers
{
    public class ProductCreatedConsumer:IConsumer<ProductCreatedEvent>
    {
        private readonly IStockService _stockService;

        public ProductCreatedConsumer(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            // 1. Gelen mesajdaki ProductId ile zaten bir stok kaydı var mı kontrol et
            var existingStock = await _stockService.TGetByProductIdAsync(context.Message.ProductId);

            if (existingStock == null)
            {
                // 2. Eğer yoksa, yeni bir stok kaydı oluştur (Başlangıç adedi: 0)
                await _stockService.TInsertAsync(new EStock
                {
                    ProductId = context.Message.ProductId,
                    Count = 0
                });

                Console.WriteLine($">>>>> [STOCK]: {context.Message.ProductName} için 0 adetli stok kaydı başarıyla açıldı.");
            }
        }
    }
}
