using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T:class
    {
        Task InsertAsync(T Entity);
        Task UpdateAsync(T Entity);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

    }
}
