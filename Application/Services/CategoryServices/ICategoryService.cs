using DTOs.CategoryDTOs;
using DTOs.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Category;

namespace Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        public Task<ResultView<IEnumerable<CategoryDTO>>> GetAllCategoriesAsync();
        public Task<ResultView<CategoryDTO>> GetCategoryByIdAsync(int id);
        public Task<ResultView<IEnumerable<CategoryDTO>>> GetCategoryByNameAsync(string categoryName);

        public Task<ResultView<string>> AddCategoryAsync(CategoryDTO createCategoryDto, IFormFile imageFile);
        public Task<ResultView<string>> UpdateCategoryAsync(int id, CategoryDTO categoryDto, IFormFile imageFile);
        public Task<ResultView<string>> DeleteCategoryAsync(int id);
    }
}
