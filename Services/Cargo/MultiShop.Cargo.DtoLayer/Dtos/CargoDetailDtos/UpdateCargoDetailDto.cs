using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class UpdateCargoDetailDto
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; }
        public string Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public int CargoCustomerId { get; set; }
        public string VendorId { get; set; }
        public int OrderingId { get; set; }
    }
}
