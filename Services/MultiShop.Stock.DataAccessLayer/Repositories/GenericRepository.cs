using MongoDB.Driver;
using MultiShop.Stock.DataAccessLayer.Abstract;
using MultiShop.Stock.EntityLayer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IDatabaseSettings _databaseSettings, string collectionName)
        {
            var client=new MongoClient(_databaseSettings.ConnectionString);
            var database=client.GetDatabase(_databaseSettings.DatabaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task InsertAsync(T entity) => await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(Expression<Func<T, bool>> filter, T entity) =>
            await _collection.ReplaceOneAsync(filter, entity);

        public async Task DeleteAsync(Expression<Func<T, bool>> filter) =>
            await _collection.DeleteOneAsync(filter);

        public async Task<List<T>> GetAllAsync() =>
            await _collection.Find(x => true).ToListAsync();

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter) =>
            await _collection.Find(filter).FirstOrDefaultAsync();
    }
}
