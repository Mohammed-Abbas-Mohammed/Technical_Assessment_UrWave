using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.General
{
    public interface IGenericRepository<T>
    {
        public Task<IQueryable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(int id);
    }
}
