using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; } 
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
        public int CargoCustomerId { get; set; }
        public CargoCustomer CargoCustomer { get; set; }
        public string VendorId { get; set; }
        public int OrderingId { get; set; }
        public CargoStatus CurrentStatus { get; set; }
    }
}
