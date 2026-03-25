using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingPaymentStatusCommandHandler:IRequestHandler<UpdateOrderingPaymentStatusCommand>
    {
        private readonly IRepository<Ordering> _repository;

        public UpdateOrderingPaymentStatusCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderingPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            // BasketId senin Ordering tablandaki 'Id' ise int'e çeviriyoruz
            // Eğer OrderingId Guid ise int.Parse yerine Guid.Parse kullanmalısın
            if (int.TryParse(request.BasketId, out int orderingId))
            {
                var values = await _repository.GetByIdAsync(orderingId);

                if (values != null)
                {
                    // Burada Ordering Entity'nde bir 'Status' alanı olduğunu varsayıyorum
                    // 1 = Ödendi/Onaylandı anlamına gelebilir
                    // values.OrderStatus = 1; 
                    
                    await _repository.UpdateAsync(values);
                }
            }
        }
    }
}
