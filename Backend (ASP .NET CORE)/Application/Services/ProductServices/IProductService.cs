using DTOs.ProductDTOs;
using DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices
{
    public interface IProductService
    {
        Task<ResultView<ProductDTO>> CreateProductAsync(ProductDTO productDto);
        Task<ResultView<ProductDTO>> UpdateProductAsync(ProductDTO productDto);
        Task<ResultView<ProductDTO>> DeleteProductAsync(Guid id);
        Task<ResultView<ProductDTO>> GetProductByIdAsync(Guid id);
        Task<ResultView<IEnumerable<ProductDTO>>> GetAllProductsAsync();
        Task<ResultView<IEnumerable<ProductDTO>>> SearchProductsByNameAsync(string name);
        public Task<EntityPaginated<ProductDTO>> GetAllPaginatedAsync(int pageNumber, int Count);
        public Task DeleteProductsBatchAsync(Guid[] ids);
    }
}
