using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Order.Persistence.Repositories
{
    
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly OrderContext _orderContext;

        public OrderDetailRepository(OrderContext orderContext) : base(orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByVendorId(string vendorId)
        {
            return await _orderContext.OrderDetails
                .Where(x => x.VendorId == vendorId)
                .ToListAsync();
        }
    }
}