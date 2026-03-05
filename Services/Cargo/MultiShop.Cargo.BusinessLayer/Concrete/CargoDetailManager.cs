using AutoMapper;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoOperationDal _cargoOperationDal;
        private readonly ICargoDetailDal _cargoDetailDal;
        private readonly IMapper _mapper;
        public CargoDetailManager(ICargoDetailDal cargoDetailDal, IMapper mapper, ICargoOperationDal cargoOperationDal)
        {
            _cargoDetailDal = cargoDetailDal;
            _mapper = mapper;
            _cargoOperationDal = cargoOperationDal;
        }

        public async Task<ResultCargoDetailDto> TChangeCargoStatus(int cargoDetailId, CargoStatus newStatus)
        {
            var cargo = await _cargoDetailDal.GetById(cargoDetailId);
            if (cargo==null)
            {
                return null;
            }
            cargo.CurrentStatus = newStatus;
            await _cargoDetailDal.Update(cargo);

            var operation = new CargoOperation
            {
                Barcode = cargo.Barcode.ToString(),
                Description = $"Kargo durumu güncellendi: {newStatus}",
                OperationDate = DateTime.Now,
            };
            await _cargoOperationDal.Insert(operation);
            return _mapper.Map<ResultCargoDetailDto>(cargo);
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
