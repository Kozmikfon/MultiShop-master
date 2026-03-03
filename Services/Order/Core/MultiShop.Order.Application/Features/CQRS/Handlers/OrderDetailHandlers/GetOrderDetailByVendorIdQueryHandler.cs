using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailByVendorIdQueryHandler
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetOrderDetailByVendorIdQueryHandler(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task<List<GetOrderDetailQueryResult>> Handle(GetOrderDetailByVendorIdQuery query)
        {          
            var values = await _orderDetailRepository.GetOrderDetailsByVendorId(query.VendorId);

            return values.Select(x => new GetOrderDetailQueryResult
            {
                OrderDetailId = x.OrderDetailId,
                ProductAmount = x.ProductAmount,
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductTotalPrice = x.ProductTotalPrice,
                OrderingId = x.OrderingId,
                VendorId = x.VendorId,
                UserId=x.Ordering.UserId,
                OrderDate=x.Ordering.OrderDate
            }).ToList();
        }
    }

}
