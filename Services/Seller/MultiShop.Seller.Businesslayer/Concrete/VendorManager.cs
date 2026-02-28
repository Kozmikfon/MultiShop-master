using MultiShop.Seller.Businesslayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.Businesslayer.Concrete
{
    public class VendorManager : IVendorService
    {
        private readonly IVendorDal _vendorDal;
        public VendorManager(IVendorDal vendorDal)
        {
            _vendorDal = vendorDal; 
        }
        public async Task TDeleteAsync(int id)
        {
           await _vendorDal.DeleteAsync(id);
        }

        public async Task<List<Vendor>> TGetAllAsync()
        {
           return await _vendorDal.GetAllAsync();
        }

        public async Task<Vendor> TGetByIdAsync(int id)
        {
            return await _vendorDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Vendor entity)
        {
            await _vendorDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(Vendor entity)
        {
            await _vendorDal.UpdateAsync(entity);
        }
    }
}
