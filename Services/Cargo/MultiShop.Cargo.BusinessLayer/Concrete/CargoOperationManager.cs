using AutoMapper;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;
        private readonly IMapper _mapper;
        public CargoOperationManager(ICargoOperationDal cargoOperationDal, IMapper mapper)
        {
            _cargoOperationDal = cargoOperationDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
            await _cargoOperationDal.Delete(id);
        }

        public async Task<List<ResultCargoOperationDto>> TGetAllAsync()
        {
            var values = await _cargoOperationDal.GetAll();
            return _mapper.Map<List<ResultCargoOperationDto>>(values);
        }

        public async Task<GetByIdCargoOperaitonDto> TGetByIdAsync(int id)
        {
            var value = await _cargoOperationDal.GetById(id);
            return _mapper.Map<GetByIdCargoOperaitonDto>(value);
        }

        public async Task TInsertAsync(CreateCargoOperationDto createDto)
        {
            var value = _mapper.Map<CargoOperation>(createDto);
            await _cargoOperationDal.Insert(value);
        }

        public async Task TUpdateAsync(UpdateCargoOperationDto updateDto)
        {
            var value = _mapper.Map<CargoOperation>(updateDto);
            await _cargoOperationDal.Update(value);
        }
    }
}