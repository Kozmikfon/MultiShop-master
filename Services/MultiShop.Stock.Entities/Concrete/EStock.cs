using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.EntityLayer.Concrete
{
    public class EStock
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StockId { get; set; }

        public string ProductId { get; set; } // Catalog'dan gelen ID

        public int Count { get; set; } // Stok adedi
    }
}
