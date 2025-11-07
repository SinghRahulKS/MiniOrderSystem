using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductService.Repository;
using ProductService.Repository.Seed;
using ProductService.Service;
using System;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ Add Controllers
builder.Services.AddControllers();

// ✅ 2️⃣ Configure PostgreSQL Database
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);

// ✅ 3️⃣ Add AutoMapper (scans all profiles in assembly)
builder.Services.AddAutoMapper(typeof(Program));

// ✅ 4️⃣ Register Services and Repositories
builder.Services.AddScoped<IProductService, ProductService.Service.ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ✅ 5️⃣ Add Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductService API",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API with Swagger in .NET 9"
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    await ProductSeeder.SeedAsync(dbContext);
}

// ✅ 6️⃣ Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService API V1");
        c.RoutePrefix = string.Empty; // Swagger at root
    });
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
