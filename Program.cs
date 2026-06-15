using Microsoft.EntityFrameworkCore;
using GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Application.Services;
using GMS_Backend.Data;
using GMS_Backend.Services.Interfaces;
using GMS_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Entity Framework + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultPostgres")
    )
);

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
//builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SubcategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();