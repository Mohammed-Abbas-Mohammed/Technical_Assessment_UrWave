using Application.Contracts.General;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.General
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MSysDbContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(MSysDbContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(dbSet.Select(p => p));

        public virtual async Task<T> GetByIdAsync(int id) => await dbSet.FindAsync(id);

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
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}

