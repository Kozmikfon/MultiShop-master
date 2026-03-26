using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface ICargoDetailDal : IGenericDal<CargoDetail>
    {
        Task<List<CargoDetail>> GetCargoDetailsByVendorId(string vendorId);
        Task<CargoDetail> GetCargoDetailWithCompany(int cargoDetailId);
    }
}
