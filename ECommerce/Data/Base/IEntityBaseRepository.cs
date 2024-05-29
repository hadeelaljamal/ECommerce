using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ECommerce.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class,IBaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include );
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[] include);
        Task CreatAsync(T entety);
        //Task UpdateAsync(int id,Category entety);
        Task UpdateAsync(T entety);

        Task DeleteAsync(int id);
        Task SaveChanges();
    }
}
