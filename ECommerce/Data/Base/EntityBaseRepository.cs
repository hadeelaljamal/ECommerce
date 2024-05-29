using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce.Data.Base
{
    public class EntityBaseRepository <T> : IEntityBaseRepository<T> where T : class,IBaseEntity
    {
        private readonly ECommerceDbContext _context;
        private readonly DbSet<T> _entities;
        public EntityBaseRepository(ECommerceDbContext context)
        {

            _context = context;
            _entities = _context.Set<T>();
    }
        public async Task CreatAsync(T entety)
        {
            await _entities.AddAsync(entety);
            // await _context.SaveChangesAsync();
            await SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var entityId = await _entities.FirstOrDefaultAsync(x => x.Id == id);
            if (entityId != null)
            {
                _entities.Remove(entityId);
                await SaveChanges();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
       // => await _context.Set<T>().ToListAsync();
       => await _entities.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include) 
        {
            IQueryable<T> query = _entities.AsQueryable();
            query = include.Aggregate(query,(current,include)=> current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        //=> await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id==id);
        =>await _entities.FirstOrDefaultAsync(x=>x.Id==id);

        public async Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _entities.AsQueryable();
            query = include.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entety)
        {
            EntityEntry entityEntry=_context.Entry<T>(entety);
            entityEntry.State= EntityState.Modified;
            await SaveChanges();
        }
    }

}
