using AutoMapper;
using DTOs.CategoryDTOs;
using DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Category;
using Technical_Assessment_Overview.Product;

namespace Application.Mapper
{
    internal class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductP, ProductDTO>().ReverseMap();
            CreateMap<CategoryC, CategoryDTO>().ReverseMap();


        }
    }
}
