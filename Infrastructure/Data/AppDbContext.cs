using GMS_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GMS_Backend.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Subcategory> Subcategories { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }

    public DbSet<Favorite> Favorites { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category -> Subcategory (1:N)
        modelBuilder.Entity<Subcategory>()
            .HasOne(sc => sc.Category)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(sc => sc.CategoryId);

        // Product -> Category (N:1)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        // Product -> Subcategory (N:1)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Subcategory)
            .WithMany()
            .HasForeignKey(p => p.SubcategoryId);

        // Feedback -> Product (N:1)
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Product)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.ProductId);

        // Feedback -> User (N:1)
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        // Favorite (User <-> Product)
        modelBuilder.Entity<Favorite>()
            .HasKey(f => new { f.UserId, f.ProductId });

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Product)
            .WithMany()
            .HasForeignKey(f => f.ProductId);

        // CartItem (User <-> Product)
        modelBuilder.Entity<CartItem>()
            .HasKey(c => new { c.UserId, c.ProductId });

        modelBuilder.Entity<CartItem>()
            .HasOne(c => c.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId);
            
        modelBuilder.Entity<PasswordResetToken>()
            .HasOne(x => x.User)
            .WithMany(u => u.PasswordResetTokens)
            .HasForeignKey(x => x.UserId);
        }
}