using Application.Contracts.Product;
using AutoMapper;
using DTOs.ProductDTOs;
using DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Product;
using Technical_Assessment_Overview.Shared;

namespace Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository _productRepository, IMapper _mapper)
        {
            productRepository = _productRepository;
            mapper = _mapper;
        }


        public async Task<ResultView<ProductDTO>> CreateProductAsync(ProductDTO productDto)
        {

            if (productDto.Price < 0 || productDto.StockQuantity < 0)
                return ResultView<ProductDTO>.Failure("Positive Value required.");

            var product = mapper.Map<ProductP>(productDto);
            var createdProduct = await productRepository.AddAsync(product);

            return ResultView<ProductDTO>.Success(mapper.Map<ProductDTO>(createdProduct));
        }

        public async Task<ResultView<ProductDTO>> UpdateProductAsync(ProductDTO productDto)
        {
            if (productDto == null)
                return ResultView<ProductDTO>.Failure("Product must have data to be added");

            var existingProduct = await productRepository.GetByIdAsync(productDto.Id);

            if (existingProduct == null)
                return ResultView<ProductDTO>.Failure("Product not found. Unable to update.");

            if (productDto.Price < 0 || productDto.StockQuantity < 0)
                return ResultView<ProductDTO>.Failure("Positive Value required.");

            mapper.Map(productDto, existingProduct);

            await productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDTO>.Success(productDto);
        }

        public async Task<ResultView<ProductDTO>> DeleteProductAsync(Guid id)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return ResultView<ProductDTO>.Failure("Product not found. Unable to delete.");

            existingProduct.Status = EntityStatus.Discontinued;

            await productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDTO>.Success(null);
        }

        public async Task<ResultView<ProductDTO>> GetProductByIdAsync(Guid id)
        {

            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
                return ResultView<ProductDTO>.Failure("Product not found.");

            var productDto = mapper.Map<ProductDTO>(product);
            return ResultView<ProductDTO>.Success(productDto);
        }

        public async Task<ResultView<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllAsync();
            var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);
            return ResultView<IEnumerable<ProductDTO>>.Success(productDtos);
        }

        public async Task<ResultView<IEnumerable<ProductDTO>>> SearchProductsByNameAsync(string name)
        {
            if (name == null)
                return ResultView<IEnumerable<ProductDTO>>.Failure("Try Again");
            var products = await productRepository.SearchByNameAsync(name);
            var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);

            return ResultView<IEnumerable<ProductDTO>>.Success(productDtos);
        }


        public async Task<EntityPaginated<ProductDTO>> GetAllPaginatedAsync(int pageNumber, int Count)
        {
            var products = await productRepository.GetAllAsync();
            var totalCount = products.Count();

            var Resultproducts = products
                .Skip(Count * (pageNumber - 1))
                .Take(Count)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    Name = p.Name,
                }).ToList();

            return new EntityPaginated<ProductDTO>
            {
                Data = Resultproducts,
                CountAllItems = totalCount
            };

        }

        public async Task DeleteProductsBatchAsync(Guid[] ids)
        {
            var products = await productRepository.GetAllAsync();

            var productsToDelete = products.Where(p => ids.Contains(p.Id)).ToList();

            if (!productsToDelete.Any())
            {
                throw new KeyNotFoundException("No products found with the provided IDs.");
            }

            await productRepository.DeleteRangeAsync(productsToDelete);
        }
    }
}
