using Microsoft.EntityFrameworkCore;
using MultiShop.Seller.DataAccessLayer.Abstract;
using MultiShop.Seller.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly SellerContext _context;

        public GenericRepository(SellerContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var value=await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(value);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T Entity)
        {
           await _context.Set<T>().AddAsync(Entity);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T Entity)
        {
           _context.Set<T>().Update(Entity);
           await _context.SaveChangesAsync();
        }
    }
}
