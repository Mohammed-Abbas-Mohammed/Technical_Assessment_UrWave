using Application.Contracts.General;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Shared;

namespace Infrastructure.General
{
    public class GenericRepositoryWithLogging<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MSysDbContext context;
        
        protected readonly DbSet<T> dbSet;

        public GenericRepositoryWithLogging(MSysDbContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(dbSet.Select(p => p).Where(p => !p.IsDeleted));

        public virtual async Task<T> GetByIdAsync(int id) => await dbSet.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        public async Task<T> AddAsync(T entity)
        {
            var recieved = (await dbSet.AddAsync(entity)).Entity;
            await context.SaveChangesAsync();
            return recieved;
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true; ;
                await UpdateAsync(entity);
            }
        }
    }
}
