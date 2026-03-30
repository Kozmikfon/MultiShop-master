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
        private readonly IShipinkService _shipinkService;
        private readonly IMapper _mapper;
        public CargoDetailManager(ICargoDetailDal cargoDetailDal, IMapper mapper, ICargoOperationDal cargoOperationDal, IShipinkService shipinkService)
        {
            _cargoDetailDal = cargoDetailDal;
            _mapper = mapper;
            _cargoOperationDal = cargoOperationDal;
            _shipinkService = shipinkService;
        }

        public async Task<ResultCargoDetailDto> TChangeCargoStatus(int cargoDetailId, CargoStatus newStatus)
        {
            var cargo = await _cargoDetailDal.GetById(cargoDetailId);
            if (cargo == null) return null;

            cargo.CurrentStatus = newStatus;
            await _cargoDetailDal.Update(cargo);

            var operation = new CargoOperation
            {
                Barcode = cargo.Barcode.ToString(),
                Description = $"Kargo durumu güncellendi: {newStatus}",
                OperationDate = DateTime.Now,
            };
            await _cargoOperationDal.Insert(operation);

            // Shipink Senkronizasyonu
            try
            {
                // Enum'u string'e çevirip gönderiyoruz
                await _shipinkService.UpdateStatusAsync(cargo.ShipinkOrderId, newStatus.ToString());
            }
            catch (Exception ex)
            {
                // Loglama...
            }

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
            var value = _mapper.Map<CargoDetail>(createDto);
            value.CurrentStatus = CargoStatus.Created;

            // 1. Önce kendi veritabanımıza kaydediyoruz
            await _cargoDetailDal.Insert(value);

            Console.WriteLine($">>>>> YENİ KARGO ID: {value.CargoDetailId}");

            // 2. Kayıt başarılıysa Shipink'e ilk bildirimi yapıyoruz
            try
            {
                var trackingNumber = await _shipinkService.CreateShipmentAsync(value.CargoDetailId);

                // 3. Eğer Shipink bir numara döndüyse, kargoyu bu numarayla güncelliyoruz
                if (!string.IsNullOrEmpty(trackingNumber))
                {
                    value.TrackingNumber = trackingNumber;
                    value.CurrentStatus = CargoStatus.LabelCreated;

                    await _cargoDetailDal.Update(value);
                    Console.WriteLine("✅ SHIPINK TAKİP NO ALINDI: " + trackingNumber);
                }
            }
            catch (Exception ex)
            {
                // API hatası olsa bile kargo veritabanımızda kalır, numara boş kalır.
                Console.WriteLine($">>>>> SHIPINK API HATASI: {ex.Message}");
            }
        }

        public async Task TUpdateAsync(UpdateCargoDetailDto updateDto)
        {
            var value = _mapper.Map<CargoDetail>(updateDto);
            await _cargoDetailDal.Update(value);
        }

        public async Task<string> TCreateShipmentProcessAsync(int cargoDetailId)
        {
            // 1. Veriyi ilişkileriyle birlikte çek (Kötü kod: Tekrar tekrar DB'ye gitme)
            var cargo = await _cargoDetailDal.GetCargoDetailWithCompany(cargoDetailId);

            if (cargo == null)
                return "Hata: Kargo kaydı veritabanında bulunamadı.";

            if (cargo.CargoCompany == null)
                return "Hata: Kargoya atanmış geçerli bir taşıyıcı firma (Company) bulunamadı.";

            // 2. Shipink Servisini Çağır (Zaten CargoDetail nesnesi alan versiyonu yazmıştık)
            // Bu metot kendi içinde Update işlemini de yapacak.
            try
            {
                var result = await _shipinkService.CreateShipmentAsync(cargoDetailId);
                return result;
            }
            catch (Exception ex)
            {
                return $"Shipink İşlem Hatası: {ex.Message}";
            }
        }
    }
}
