using Microsoft.EntityFrameworkCore;
using GMS_Backend.Infrastructure.Repositories;
using DotNetEnv;
using GMS_Backend.Application.Services;
using GMS_Backend.Infrastructure.Data;
using GMS_Backend.Domain.Repositories;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["Jwt:Key"];

var connectionString = builder.Configuration.GetConnectionString("DefaultPostgres");

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5041", "http://192.168.1.82:4200")
            .AllowAnyHeader() 
            .AllowAnyMethod(); 
    });
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controllers
builder.Services.AddControllers();

// Entity Framework + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SubcategoryService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}



app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll"); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();