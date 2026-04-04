using MultiShop.Order.Domain.Entities.Enums;
using MultiShop.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Results.OrderingResults
{
    public class GetOrderingQueryResult
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int AddressId { get; set; }
        public double TotalWeight { get; set; }
        public string ShipinkOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string? TrackingNumber { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    }
}
