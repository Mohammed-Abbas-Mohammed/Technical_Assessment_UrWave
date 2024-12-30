using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Shared;

namespace Technical_Assessment_Overview.Product
{
    public class ProductP:BaseEntity
    {
        [Required, MaxLength(100, ErrorMessage = "Name is required less than 100 chars")]
        public string Name { get; set; }

        [Required, MaxLength(500, ErrorMessage = "Description is required less than 500 chars")]
        public string Description { get; set; }

        [Required, Column(TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Product price must be a positive value.")]
        public decimal Price { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a positive value.")]
        public int StockQuantity { get; set; }

        [MaxLength(250)]
        public string? Url { get; set; }

        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
