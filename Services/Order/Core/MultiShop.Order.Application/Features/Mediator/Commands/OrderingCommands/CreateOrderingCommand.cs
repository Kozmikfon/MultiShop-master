    using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
    {
        public class CreateOrderingCommand : IRequest
        {
            public string UserId { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime OrderDate { get; set; }

            public string SenderCustomer { get; set; }
            public int CargoCompanyId { get; set; }
            public int CargoCustomerId { get; set; }

            public int AddressId { get; set; }
            public double Weight { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Length { get; set; }
            public string ReceiverName { get; set; }
            public string ReceiverSurname { get; set; }
            public string ReceiverEmail { get; set; }
            public string ReceiverPhone { get; set; }

            public string VendorId { get; set; }
            public List<CreateOrderDetailCommand> OrderDetails { get; set; }
    }
    }
