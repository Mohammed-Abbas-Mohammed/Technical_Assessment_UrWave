using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Technical_Assessment_Overview.Shared;

namespace DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        //public IFormFile? ImageFile { get; set; }

        public string? ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public EntityStatus Status { get; set; }
    }
}
