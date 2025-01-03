
using Application.Contracts.Category;
using Application.Contracts.Product;
using Application.Mapper;
using Application.Services.CategoryServices;
using Application.Services.ProductServices;
using AutoMapper;
using DTOs.CategoryDTOs;
using DTOs.ProductDTOs;
using Infrastructure.CategoryInfrastructure;
using Infrastructure.ProductInfrastructre;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;
using System;

namespace ProductCategoryManagementSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MSysDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            var serviceProvider = builder.Services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();


            builder.Services.AddCors(op =>
            {

                op.AddPolicy("Default", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
                });
            });


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register services for dependency injection
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("Default");

            app.UseAuthorization();

            #region Minimal APIs

            //product

            app.MapGet("/products", async (IProductService productService) =>
            {
                try
                {
                    var products = await productService.GetAllProductsAsync();
                    return Results.Ok(products);
                }
                catch

                    (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            app.MapGet("/products/{id}", async (Guid id, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return Results.Ok(product);
            });

            app.MapPost("/products", async (ProductDTO productDto, IProductService productService) =>
            {
                var product = await productService.CreateProductAsync(productDto);
                return Results.Created($"/products/{product.Entity.Id}", product);
            });

            app.MapPut("/products/{id}", async (Guid id,ProductDTO productDto, IProductService productService) =>
            {
                var product = await productService.UpdateProductAsync(productDto);
                return Results.NoContent();
            });

            app.MapDelete("/products/{id}", async (Guid id, IProductService productService) =>
            {
                await productService.DeleteProductAsync(id);
                return Results.NoContent();
            });

            app.MapDelete("/products/batch", async (Guid[] ids, IProductService productService) =>
            {
                try
                {
                    await productService.DeleteProductsBatchAsync(ids);
                    return Results.Ok($"Successfully deleted {ids.Length} product(s).");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });



            //Category

            app.MapGet("/categories", async (ICategoryService caegoryService) =>
            {
                var categories = await caegoryService.GetAllCategoriesAsync();
                return Results.Ok(categories);
            });

            app.MapGet("/categories/{id}", async (Guid id, ICategoryService caegoryService) =>
            {
                var category = await caegoryService.GetCategoryByIdAsync(id);
                return Results.Ok(category);
            });

            app.MapPost("/categories", async (CategoryDTO categoryDto, ICategoryService caegoryService) =>
            {
                var category = await caegoryService.AddCategoryAsync(categoryDto);
                return Results.Created($"/categories/{category.Entity.Id}", category);
            });

            app.MapPut("/categories/{id}", async (Guid id, CategoryDTO categoryDto, ICategoryService caegoryService) =>
            {
                var category = await caegoryService.UpdateCategoryAsync(id,categoryDto);
                return Results.NoContent();
            });

            app.MapDelete("/categories/{id}", async (Guid id, ICategoryService caegoryService) =>
            {
                await caegoryService.DeleteCategoryAsync(id);
                return Results.NoContent();
            });

            #endregion



           

            app.Run();
        }
    }
}
