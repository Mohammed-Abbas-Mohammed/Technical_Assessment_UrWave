using Application.Contracts.Product;
using DTOs.Shared;
using Infrastructure.General;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Product;

namespace Infrastructure.ProductInfrastructre
{
    public class ProductRepository : GenericRepositoryWithLogging<ProductP>, IProductRepository
    {
        protected readonly MSysDbContext context;

        public ProductRepository(MSysDbContext _context) : base(_context)
        {
            context = _context;
        }


        public async Task<IQueryable<ProductP>> SearchByNameAsync(string name)
        {
            var products = await GetAllAsync();
            return products;
            //return products.Where(p => p.Translations.Any(t => t.Name.ToLower().Contains(name.ToLower()) ||
            //     p.Translations.Any(t => t.BrandName.ToLower().Contains(name.ToLower())))).Include(p => p.Translations).Include(p => p.Images);
        }

        public override async Task<ProductP> GetByIdAsync(int id)
        {
            return await context.products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IQueryable<ProductP>> GetAllAsync()
        {
            var products = context.products.Where(p => !p.IsDeleted);
            return products;
        }

        public async Task<IQueryable<ProductP>> GetFilteredProductsAsync(int languageId)
        {
            var products = context.products.Where(p => !p.IsDeleted);
            return products;
        }
        public async Task<EntityPaginated<ProductP>> GetAllPaginatedAsync(int pageNumber, int count)
        {

            if (pageNumber < 1) pageNumber = 1;
            if (count < 1) count = 10;


            var skip = (pageNumber - 1) * count;

            var query = context.products
                                .OrderBy(p => p.Id)
                                .Skip(skip).Take(count);
            var totalCount = await context.products.CountAsync();


            var products = await query.ToListAsync();


            var result = new EntityPaginated<ProductP>
            {
                Data = products,        // Paginated items
                PageNumber = pageNumber,  // Current page number
                Count = count,            // Items per page
                CountAllItems = totalCount   // Total items in the database
            };

            return result;
        }
    }
}
