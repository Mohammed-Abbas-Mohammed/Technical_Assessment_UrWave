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
using Technical_Assessment_Overview.Shared;

namespace Infrastructure.ProductInfrastructre
{
    public class ProductRepository : GenericRepository<ProductP>, IProductRepository
    {
        protected readonly MSysDbContext context;

        public ProductRepository(MSysDbContext _context) : base(_context)
        {
            context = _context;
        }


        public async Task<IEnumerable<ProductP>> SearchByNameAsync(string name)
        {
            var products = await context.products
                                 .Where(t => t.Name.ToLower().Contains(name.ToLower()))
                                 .Select(t => new ProductP
                                 {
                                     Id = t.Id,
                                     Name = t.Name,
                                     Description = t.Description,
                                     Price = t.Price,
                                     StockQuantity = t.StockQuantity,
                                     ImageUrl = t.ImageUrl,
                                     CategoryId = t.CategoryId,
                                     Status = t.Status,
                                     CreatedDate = t.CreatedDate,
                                     UpdatedDate = t.UpdatedDate
                                 })
                                 .ToListAsync();  // Materialize the query
            return products;

        }

        public async Task<IQueryable<ProductP>> GetFilteredProductsAsync()
        {
            var products = context.products.Where(p => p.Status != EntityStatus.Discontinued);
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
