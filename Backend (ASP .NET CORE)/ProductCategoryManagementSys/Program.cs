
using Application.Contracts.Category;
using Application.Contracts.Product;
using Application.Services.CategoryServices;
using Application.Services.ProductServices;
using DTOs.CategoryDTOs;
using DTOs.ProductDTOs;
using Infrastructure.CategoryInfrastructure;
using Infrastructure.ProductInfrastructre;
using Microsoft.EntityFrameworkCore;
using ProjectDbContext;

namespace ProductCategoryManagementSys
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MSysDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register services for dependency injection
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
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

            app.UseAuthorization();

            #region Minimal APIs

            //product

            app.MapGet("/products", async (IProductService productService) =>
            {
                var products = await productService.GetAllProductsAsync();
                return Results.Ok(products);
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



            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.Run();
        }
    }
}