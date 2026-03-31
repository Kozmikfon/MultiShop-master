using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
      
    }
}
