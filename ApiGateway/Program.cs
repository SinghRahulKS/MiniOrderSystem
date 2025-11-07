using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1?? Load Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2?? Register Ocelot
builder.Services.AddOcelot(builder.Configuration);

// 3?? Enable Allow-All CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4?? Use middlewares in correct order
app.UseCors("AllowAll");
app.UseHttpsRedirection();

// 5?? Ocelot Middleware (must be last)
await app.UseOcelot();

app.Run();
