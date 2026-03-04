using AutoMapper;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;
        private readonly IMapper _mapper;
        public CargoDetailManager(ICargoDetailDal cargoDetailDal, IMapper mapper)
        {
            _cargoDetailDal = cargoDetailDal;
            _mapper = mapper;
        }

        public async Task TDeleteAsync(int id)
        {
           await _cargoDetailDal.Delete(id);
        }

        public async Task<List<ResultCargoDetailDto>> TGetAllAsync()
        {
            var value=await _cargoDetailDal.GetAll();
            return _mapper.Map<List<ResultCargoDetailDto>>(value);
        }

        public async Task<GetByIdCargoDetailDto> TGetByIdAsync(int id)
        {
            var value = await _cargoDetailDal.GetById(id);
            return _mapper.Map<GetByIdCargoDetailDto>(value);
        }

        public async Task<List<ResultCargoDetailDto>> TGetCargoDetailsByVendorId(string vendorId)
        {
            var value = await _cargoDetailDal.GetCargoDetailsByVendorId(vendorId);
            return _mapper.Map<List<ResultCargoDetailDto>>(value);
        }

        public async Task TInsertAsync(CreateCargoDetailDto createDto)
        {
           var value=_mapper.Map<CargoDetail>(createDto);
            await _cargoDetailDal.Insert(value);
        }

        public async Task TUpdateAsync(UpdateCargoDetailDto updateDto)
        {
            var value = _mapper.Map<CargoDetail>(updateDto);
            await _cargoDetailDal.Update(value);
        }
    }
}
