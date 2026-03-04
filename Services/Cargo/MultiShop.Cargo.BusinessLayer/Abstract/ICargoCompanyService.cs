using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoCompanyService : IGenericService<ResultCargoCompanyDto,CreateCargoCompanyDto,UpdateCargoCompanyDto,GetByIdCargoCompanyDto>
    {
    }
}
