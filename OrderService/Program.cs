using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderService.Data;
using OrderService.Repository;
using OrderService.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ Add Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true; // Optional (for pretty JSON)
    });

// ✅ 2️⃣ Configure PostgreSQL Database
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention()
);

// ✅ 3️⃣ Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//// ✅ 4️⃣ Register Repositories & Services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService.Service.OrderService>();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();

//// ✅ 5️⃣ Register HTTP Clients for other microservices
//builder.Services.AddHttpClient("UserService", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:UserService"]);
//});

//builder.Services.AddHttpClient("ProductService", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductService"]);
//});

//builder.Services.AddScoped<UserClient>();
//builder.Services.AddScoped<ProductClient>();

// ✅ 6️⃣ CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ✅ 7️⃣ Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OrderService API",
        Version = "v1",
        Description = "Order & Basket Management API in .NET 9"
    });
});

// ✅ 8️⃣ Build App
var app = builder.Build();

// ✅ 9️⃣ Apply migrations / seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    await BasketSeeder.SeedAsync(dbContext);  // 🛒 Seed basket first
    await OrderSeeder.SeedAsync(dbContext);
}

// ✅ 🔟 Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
