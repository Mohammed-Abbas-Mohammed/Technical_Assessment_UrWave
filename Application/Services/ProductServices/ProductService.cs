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
            if (productDto.Id > 0)
                return ResultView<ProductDTO>.Failure("Product already exists. Use update to modify it.");

            if (productDto.Price < 0)
                return ResultView<ProductDTO>.Failure("Product price must be a positive value.");

            if (productDto.StockQuantity < 0)
                return ResultView<ProductDTO>.Failure("Product Quantity must be a positive value.");


            var product = mapper.Map<ProductP>(productDto);
            var createdProduct = await productRepository.AddAsync(product);



            return ResultView<ProductDTO>.Success(mapper.Map<ProductDTO>(createdProduct));
        }

        public async Task<ResultView<ProductDTO>> UpdateProductAsync(ProductDTO productDto)
        {
            if (productDto == null)
                return ResultView<ProductDTO>.Failure("Product must have data to be added");

            var existingProduct = await productRepository.GetByIdAsync(productDto.Id);
            if (existingProduct == null || existingProduct.IsDeleted)
                return ResultView<ProductDTO>.Failure("Product not found. Unable to update.");

            if (productDto.Price < 0)
                return ResultView<ProductDTO>.Failure("Product price must be a positive value.");

            if (productDto.StockQuantity < 0)
                return ResultView<ProductDTO>.Failure("Product Quantity must be a positive value.");

            mapper.Map(productDto, existingProduct);

            //existingProduct.UpdatedBy = _userService.GetCurrentUserId();

            await productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDTO>.Success(productDto);
        }

        public async Task<ResultView<ProductDTO>> DeleteProductAsync(int id)
        {
            var existingProduct = await productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return ResultView<ProductDTO>.Failure("Product not found. Unable to delete.");

            existingProduct.IsDeleted = true;
            //_userService.GetCurrentUserId();
            //existingProduct.Updated = DateTime.Now;

            await productRepository.UpdateAsync(existingProduct);
            return ResultView<ProductDTO>.Success(null);
        }

        public async Task<ResultView<ProductDTO>> GetProductByIdAsync(int id)
        {

            var product = await productRepository.GetByIdAsync(id);

            if (product == null)
                return ResultView<ProductDTO>.Failure("Product not found.");

            var productDto = mapper.Map<ProductDTO>(product);
            return ResultView<ProductDTO>.Success(productDto);
        }

        public async Task<ResultView<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            //var languageId = _languageService.GetCurrentLanguageCode();

            var products = await productRepository.GetAllAsync();

            //var filteredProducts = await productRepository.GetFilteredProductsAsync(languageId);

            //var filteredProducts =  products.Where(p => p.Translations.Any(t => t.Language.Id == languageId));
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
            //var languageId = _languageService.GetCurrentLanguageCode(); // Get current language
            var products = await productRepository.GetAllAsync();
            //var productsQuery = products.Where(p => p.Translations.Any(t => t.LanguageId == languageId)).AsQueryable();
            //var productsQuery = await _productRepository.GetAllAsync(); 

            var totalCount = products.Count();

            var Resultproducts = products
                .Skip(Count * (pageNumber - 1))
                .Take(Count)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CreatedBy = p.CreatedBy,
                    CreatedDate = p.CreatedDate,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedDate = p.UpdatedDate,
                    IsDeleted = p.IsDeleted
                }
                    //Images = p.Images.Select(img => new ProductImageDto
                    ).ToList();

            return new EntityPaginated<ProductDTO>
            {
                Data = Resultproducts,
                CountAllItems = totalCount
            };

        }
    }
}
