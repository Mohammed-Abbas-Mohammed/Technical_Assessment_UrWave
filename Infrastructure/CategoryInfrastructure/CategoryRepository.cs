using Application.Contracts.Category;
using Infrastructure.General;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Technical_Assessment_Overview.Category;
using Technical_Assessment_Overview.Product;

namespace Infrastructure.CategoryInfrastructure
{
    internal class CategoryRepository : GenericRepository<CategoryC>, ICategoryRepository
    {
        private readonly MSysDbContext dbContext;
        public CategoryRepository(MSysDbContext _dbContextB) : base(_dbContextB)
        {
            dbContext = _dbContextB;
        }

        public async Task<IEnumerable<CategoryC>> GetByNameAsync(string categoryName)
        {
            var categories = await dbContext.Categories
                .Where(t => t.Name.ToLower().Contains(categoryName.ToLower()))
                .Select(t => new CategoryC
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Status = t.Status,
                    CreatedDate = t.CreatedDate,
                    UpdatedDate = t.UpdatedDate
                })
                .ToListAsync();  

            return categories;
        }

        public async Task AddAsync(CategoryC category)
        {
            await dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
            }
        }

        public async Task<IEnumerable<CategoryC>> GetAllAsync()=> await dbContext.Categories.ToListAsync();

        public async Task<CategoryC> GetByIdAsync(Guid id)=> await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
 
        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();

        public async Task UpdateAsync(CategoryC category) => dbContext.Categories.Update(category);
   

        public async Task<bool> AnyAsync(Expression<Func<CategoryC, bool>> predicate)
        {
            return await dbContext.Categories.AnyAsync(predicate);
        }
    }
}
