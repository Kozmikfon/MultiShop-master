using AutoMapper;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;
        private readonly IMapper _mapper;
        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal, IMapper mapper)
        {
            _cargoCompanyDal = cargoCompanyDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
           await _cargoCompanyDal.Delete(id);
        }

        public async Task<List<ResultCargoCompanyDto>> TGetAllAsync()
        {
            var values=await _cargoCompanyDal.GetAll();
            return _mapper.Map<List<ResultCargoCompanyDto>>(values);
        }

        public async Task<GetByIdCargoCompanyDto> TGetByIdAsync(int id)
        {
            var value=await _cargoCompanyDal.GetById(id);
            return _mapper.Map<GetByIdCargoCompanyDto>(value);
        }

        public async Task TInsertAsync(CreateCargoCompanyDto createDto)
        {
            var value=_mapper.Map<CargoCompany>(createDto); 
            await _cargoCompanyDal.Insert(value);
        }

        public async Task TUpdateAsync(UpdateCargoCompanyDto updateDto)
        {
           var value=_mapper.Map<CargoCompany>(updateDto);
           await _cargoCompanyDal.Update(value);
        }
    }
}
