using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Product;
using Technical_Assessment_Overview.Shared;

namespace Technical_Assessment_Overview.Category
{
    public class CategoryC : BaseEntity
    {
        [Required, MaxLength(50, ErrorMessage = "Name is required less than 50 chars")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description is required less than 200 chars")]
        public string? Description { get; set; }

        [Required, ForeignKey("Category")]
        public Guid? ParentCategoryId { get; set; }
        public CategoryC? ParentCategory { get; set; }
        public ICollection<ProductP>? Products { get; set; }
        //public ICollection<CategoryC>? SubCategories { get; set; }


    }
}
