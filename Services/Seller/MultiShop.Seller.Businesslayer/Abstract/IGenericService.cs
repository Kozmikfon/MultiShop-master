using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Seller.Businesslayer.Abstract
{
    public interface IGenericService<TResult, TCreate, TUpdate, TGetById>
    {
        Task TInsertAsync(TCreate createDto);
        Task TUpdateAsync(TUpdate updateDto);
        Task TDeleteAsync(int id);
        Task<List<TResult>> TGetAllAsync();
        Task<TGetById> TGetByIdAsync(int id);
    }
}
