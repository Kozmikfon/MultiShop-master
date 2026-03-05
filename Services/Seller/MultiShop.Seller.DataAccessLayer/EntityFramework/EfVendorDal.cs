using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Context;
using MultiShop.Seller.DataAccessLayer.Repositories;
using MultiShop.Seller.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.DataAccessLayer.EntityFramework
{
    public class EfVendordal : GenericRepository<Vendor>, IVendorDal
    {
        public EfVendordal(SellerContext context) : base(context)
        {
        }
    }
}
