using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete.Enums
{
    public enum CargoStatus
    {
        // Kayıt oluşturuldu, henüz bir işlem yapılmadı
        Created = 1,

        // Kargo etiketi basıldı, paketleniyor
        LabelCreated = 2,

        // Kurye paketi teslim aldı
        PickedUp = 3,

        // Ana depoda veya transfer merkezinde, yolda
        InTransit = 4,

        // Dağıtım şubesine ulaştı, kurye dağıtıma çıktı
        OutForDelivery = 5,

        // Teslimat başarılı
        Delivered = 6,

        // Alıcı adreste bulunamadı veya teslimat başarısız
        Failed = 7,

        // Paket göndericiye geri döndü
        Returned = 8
    }
}
