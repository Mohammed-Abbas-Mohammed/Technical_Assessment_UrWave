using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Shared;

namespace Technical_Assessment_Overview.Category
{
    public class CategoryC : BaseEntity
    {
        [Required, MaxLength(100, ErrorMessage = "Name is required less than 100 chars")]
        public string Name { get; set; }

        [Required, MaxLength(500, ErrorMessage = "Description is required less than 500 chars")]
        public string Description { get; set; }

        [Required, ForeignKey("Category")]
        public int? ParentCategoryId { get; set; }
    }
}
