namespace GMS_Backend.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;
using GMS_Backend.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(p => p.CartItems)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Category)
            .Include(p => p.CartItems)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Subcategory)
            .Include(p => p.Favorites)
                .ThenInclude(u => u.Product)
                    .ThenInclude(p => p.Category)
            .Include(p => p.Favorites)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Subcategory)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Category)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Subcategory)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Feedbacks)
            .FirstOrDefaultAsync(
                p => p.Id == id);   
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(p => p.CartItems)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Category)
            .Include(p => p.CartItems)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Subcategory)
            .Include(p => p.Favorites)
                .ThenInclude(u => u.Product)
                    .ThenInclude(p => p.Category)
            .Include(p => p.Favorites)
                .ThenInclude(u => u.Product)
                    .ThenInclude(c => c.Subcategory)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Category)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Subcategory)
            .Include(u => u.PurchasedProducts)
                .ThenInclude(p => p.Feedbacks)
            .ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}