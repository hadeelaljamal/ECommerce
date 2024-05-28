using ECommerce.Controllers;
using ECommerce.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Data.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreatAsync(Category entety);
        //Task UpdateAsync(int id,Category entety);
        Task UpdateAsync(Category entety);

        Task DeleteAsync(int id);   
    }
}
