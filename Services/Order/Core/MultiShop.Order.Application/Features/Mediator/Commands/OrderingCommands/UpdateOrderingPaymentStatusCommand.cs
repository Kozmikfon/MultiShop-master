using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class UpdateOrderingPaymentStatusCommand:IRequest
    {
        public string BasketId { get; set; }

        // Ödeme başarılı mı?
        public bool IsPaid { get; set; }
    }
}
