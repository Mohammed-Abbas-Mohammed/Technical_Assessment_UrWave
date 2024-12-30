using Application.Contracts.Category;
using Application.Contracts.Product;
using AutoMapper;
using DTOs.CategoryDTOs;
using DTOs.ProductDTOs;
using DTOs.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper)
        {
            categoryRepository = _categoryRepository;
            mapper = _mapper;
        }


        public Task<ResultView<string>> AddCategoryAsync(CategoryDTO createCategoryDto, IFormFile imageFile)
        {
            throw new NotImplementedException();
        }
        public Task<ResultView<string>> UpdateCategoryAsync(Guid id, CategoryDTO categoryDto, IFormFile imageFile)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultView<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllAsync();
            var categoryDtos = mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return ResultView<IEnumerable<CategoryDTO>>.Success(categoryDtos);
        }

        public async Task<ResultView<CategoryDTO>> GetCategoryByIdAsync(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
                return ResultView<CategoryDTO>.Failure("Category not found.");

            var categoryDto = mapper.Map<CategoryDTO>(category);
            return ResultView<CategoryDTO>.Success(categoryDto);
        }

        public async Task<ResultView<IEnumerable<CategoryDTO>>> GetCategoryByNameAsync(string categoryName)
        {
            if (categoryName == null)
                return ResultView<IEnumerable<CategoryDTO>>.Failure("Try Again");
            var categories = await categoryRepository.GetByNameAsync(categoryName);
            var categoryDtos = mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return ResultView<IEnumerable<CategoryDTO>>.Success(categoryDtos);
        }

       
    }
}
