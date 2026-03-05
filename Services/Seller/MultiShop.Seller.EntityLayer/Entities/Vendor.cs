using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.EntityLayer.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Shopname { get; set; }
        public string TaxNumber { get; set; }
        public string? SubMerchantKey { get; set; }
        public bool IsActive { get; set; }
    }
}
