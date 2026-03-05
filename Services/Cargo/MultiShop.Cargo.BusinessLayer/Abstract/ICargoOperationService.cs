using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoOperationService : IGenericService<ResultCargoOperationDto,CreateCargoOperationDto,UpdateCargoOperationDto,GetByIdCargoOperaitonDto>
    {
        Task<List<ResultCargoOperationDto>> TGetCargoOperationsByBarcodeAsync(string barcode);
    }
}
