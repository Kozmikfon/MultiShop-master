using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoDetailDal : GenericRepository<CargoDetail>, ICargoDetailDal
    {

        public EfCargoDetailDal(CargoContext context) : base(context)
        { 
        }

        public async Task<List<CargoDetail>> GetCargoDetailsByVendorId(string vendorId)
        {
            return await _context.CargoDetails.Where(x=>x.VendorId== vendorId).ToListAsync();
        }

        public async Task<CargoDetail> GetCargoDetailWithCompany(int cargoDetailId)
        {
            return await _context.CargoDetails
             .Include(x => x.CargoCompany) // Kritik nokta: Company nesnesi null gelmesin diye
             .FirstOrDefaultAsync(x => x.CargoDetailId == cargoDetailId);
        }
    }
}
