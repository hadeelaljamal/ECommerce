using ECommerce.Controllers;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Data.Services
{
    public class CategoryServices : ICategoryServices
    {
        //First I need access to the database to do the actions 
        private readonly ECommerceDbContext _context; //referance for db
        //inject for dbcontext in the controller done by contractor
        public CategoryServices(ECommerceDbContext context) //this called constructor dependency injection
        {
            _context=context;
        }
        public async Task CreatAsync(Category entety)
        {
            await _context.Categories.AddAsync(entety);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoryId = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            //var categoryId = await _context.Categories.FindAsync(id);
            if (categoryId != null)
            {
                _context.Categories.Remove(categoryId);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<Category>> GetAllAsync()
        {
             return await _context.Categories.ToListAsync();

        }

        //public async Task<Category> GetByIdAsync(int CategoryId)

        // =>   await _context.Categories.FirstOrDefaultAsync(x=>x.CategoryId== CategoryId);


        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id); 
        }
        


        //public async Task UpdateAsync(int id,Category entety)
        //public async Task UpdateAsync(Category entety)
        //{
        //    //var _catgoryModel = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        //    var _catgoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == entety.CategoryId);

        //    if (_catgoryModel != null)
        //    {
        //        //CategoryId.CategoryId=entety.CategoryId;
        //        //_catgoryModel.Name = entety.Name;
        //        //_catgoryModel.Description = entety.Description;

        //        //يفضل هنا تعملي mapping بال DTO عشان خاطر traking 

        //        //_context.Categories.Update(new Category { Name = entety.Name,Description = entety.Description});
               
        //        _context.Categories.Update(entety);

        //        //_context.Entry(entety).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }
            
           
        //}

       
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
