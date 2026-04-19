using MultiShop.Stock.DataAccessLayer.Abstract;
using MultiShop.Stock.DataAccessLayer.Repositories;
using MultiShop.Stock.EntityLayer.Concrete;
using MultiShop.Stock.EntityLayer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.DataAccessLayer.EntityFramework
{
    public class MongoStockDal : GenericRepository<EStock>, IStockDal
    {
        public MongoStockDal(IDatabaseSettings _databaseSettings)
            : base(_databaseSettings, _databaseSettings.StockCollectionName) // Değeri buradan gönderiyoruz
        {
        }
    }
}
