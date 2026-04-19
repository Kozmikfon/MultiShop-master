using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T: class 
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync();
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
    }
}
