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
using Technical_Assessment_Overview.Category;

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
            return await dbContext.Categories
                                 //.Include(c => c.Translations)
                                 //.Where(c => (t => t.Name.ToLower().Equals(categoryName.ToLower())))
                                 .ToListAsync();
        }
       
        public async Task AddAsync(CategoryC category)
        {
            await dbContext.Categories.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
            }
        }

        public async Task<IEnumerable<CategoryC>> GetAllAsync()
        {

            return await dbContext.Categories
                                    // .Include(c => c.Translations)
                                    //.ThenInclude(t => t.Language)
                                    // .Include(c => c.ProductCategories)
            .Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<CategoryC> GetByIdAsync(int id)
        {
            return await dbContext.Categories
                                 //.Include(c => c.Translations)
                                 //.Include(c => c.ProductCategories)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryC category)
        {

            //_dbContext.Attach(category);
            //_dbContext.Entry(category).State = EntityState.Modified;
            dbContext.Categories.Update(category);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<CategoryC, bool>> predicate)
        {
            return await dbContext.Categories.AnyAsync(predicate);
        }
    }
}
