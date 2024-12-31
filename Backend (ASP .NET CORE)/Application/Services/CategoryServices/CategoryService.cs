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
using Technical_Assessment_Overview.Category;
using Technical_Assessment_Overview.Product;
using Technical_Assessment_Overview.Shared;

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


        public async Task<ResultView<CategoryDTO>> AddCategoryAsync(CategoryDTO CategoryDto)
        {
            if (CategoryDto.Status == EntityStatus.Discontinued)
                return ResultView<CategoryDTO>.Failure("Category status cann't be Discontinued");

            var category = mapper.Map<CategoryC>(CategoryDto);
            var createdCategory = await categoryRepository.AddAsync(category);

            return ResultView<CategoryDTO>.Success(mapper.Map<CategoryDTO>(createdCategory));
        }

        public async Task<ResultView<CategoryDTO>> UpdateCategoryAsync(Guid id, CategoryDTO categoryDto)
        {
            if (categoryDto == null)
                return ResultView<CategoryDTO>.Failure("Category must have data to be added");

            if (categoryDto.Status == EntityStatus.Discontinued)
                return ResultView<CategoryDTO>.Failure("Category status cann't be Discontinued");

            var existingCtaegory = await categoryRepository.GetByIdAsync(categoryDto.Id);

            if (existingCtaegory == null)
                return ResultView<CategoryDTO>.Failure("Category not found. Unable to update.");

            mapper.Map(categoryDto, existingCtaegory);

            await categoryRepository.UpdateAsync(existingCtaegory);
            return ResultView<CategoryDTO>.Success(categoryDto);
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

        public async Task<ResultView<CategoryDTO>> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
                return ResultView<CategoryDTO>.Failure("Category not found. Unable to delete.");

            if (existingCategory.Products != null)
                return ResultView<CategoryDTO>.Failure("Unable to delete.This category has products.");

            await categoryRepository.UpdateAsync(existingCategory);
            return ResultView<CategoryDTO>.Success(null);
        }
    }
}
