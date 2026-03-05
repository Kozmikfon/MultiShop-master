using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoDetailService : IGenericService<ResultCargoDetailDto,CreateCargoDetailDto,UpdateCargoDetailDto,GetByIdCargoDetailDto>
    {
        Task<List<ResultCargoDetailDto>> TGetCargoDetailsByVendorId(string vendorId);
        Task <ResultCargoDetailDto> TChangeCargoStatus(int cargoDetailId, CargoStatus newStatus);
    }
}
