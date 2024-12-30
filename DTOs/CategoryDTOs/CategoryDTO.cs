using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
