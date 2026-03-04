using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete.Enums
{
    public enum CargoStatus
    {
        Hazirlaniyor = 1,      // Satıcı kargoyu hazırlıyor
        KargoyaVerildi = 2,    // Kargo şirketine teslim edildi
        Yolda = 3,             // Transfer merkezinde / Araçta
        Subede = 4,            // Varış şubesinde
        Dagitimda = 5,         // Kurye üzerinde
        TeslimEdildi = 6,      // Alıcıya ulaştı
        IadeEdildi = 7,        // Alıcı kabul etmedi/Geri döndü
        IptalEdildi = 8        // Sipariş/Kargo iptal
    }
}
