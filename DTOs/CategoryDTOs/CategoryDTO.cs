using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Shared;

namespace DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
      
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public EntityStatus Status { get; set; }

    }
}
