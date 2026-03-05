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
    public class EfCargoOperationDal : GenericRepository<CargoOperation>, ICargoOperationDal
    {
        private readonly CargoContext _cargoContext;
        public EfCargoOperationDal(CargoContext context, CargoContext cargoContext) : base(context)
        {
            _cargoContext = cargoContext;
        }

        public async Task<List<CargoOperation>> GetOperationsByBarcode(string barcode)
        {
            return await _cargoContext.CargoOperations
                .Where(x => x.Barcode == barcode)
                .OrderByDescending(x => x.OperationDate)
                .ToListAsync();
        }
    }
}