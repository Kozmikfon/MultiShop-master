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
        private readonly CargoContext _cargoContext;

        public EfCargoDetailDal(CargoContext context,CargoContext cargoContext) : base(context)
        {
            _cargoContext = cargoContext;
        }

        public async Task<List<CargoDetail>> GetCargoDetailsByVendorId(string vendorId)
        {
            return await _cargoContext.CargoDetails.Where(x=>x.VendorId== vendorId).ToListAsync();
        }

        public async Task<CargoDetail> GetCargoDetailWithCompany(int cargoDetailId)
        {
            return await _cargoContext.CargoDetails
             .Include(x => x.CargoCompany) // Kritik nokta: Company nesnesi null gelmesin diye
             .FirstOrDefaultAsync(x => x.CargoDetailId == cargoDetailId);
        }
    }
}
