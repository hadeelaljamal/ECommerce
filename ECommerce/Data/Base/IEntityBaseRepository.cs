using ECommerce.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECommerce.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class,IBaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreatAsync(T entety);
        //Task UpdateAsync(int id,Category entety);
        Task UpdateAsync(T entety);

        Task DeleteAsync(int id);
        Task SaveChanges();
    }
}
