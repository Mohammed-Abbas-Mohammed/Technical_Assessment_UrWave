using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Assessment_Overview.Category;
using Technical_Assessment_Overview.Product;

namespace ProjectDbContext
{
    public class MSysDbContext : DbContext
    {

        public DbSet<ProductP> products { get; set; }
        public DbSet<CategoryC> Categories { get; set; }
        

        public MSysDbContext(DbContextOptions<MSysDbContext> options)
        : base(options)
        {

        }
    }
}
