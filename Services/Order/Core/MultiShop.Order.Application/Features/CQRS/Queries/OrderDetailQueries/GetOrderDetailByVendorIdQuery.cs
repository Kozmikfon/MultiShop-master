using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries
{
    public class GetOrderDetailByVendorIdQuery
    {
        public string VendorId { get; set; }

        public GetOrderDetailByVendorIdQuery(string vendorId)
        {
            VendorId = vendorId;
        }
    }
}
