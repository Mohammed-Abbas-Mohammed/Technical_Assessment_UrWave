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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Category self-referencing relationship with ON DELETE NO ACTION
            modelBuilder.Entity<CategoryC>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent deleting a category if it has associated products
            modelBuilder.Entity<CategoryC>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
