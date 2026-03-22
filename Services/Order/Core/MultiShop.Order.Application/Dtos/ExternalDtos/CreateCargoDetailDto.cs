using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Dtos.ExternalDtos
{
    public class CreateCargoDetailDto
    {
        public string SenderCustomer { get; set; }
        public string ReceiverName { get; set; } 
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public int CargoCustomerId { get; set; }
        public string VendorId { get; set; }
        public int OrderingId { get; set; }
    }
}
