using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Data;

namespace GMS_Backend.Infrastructure.Repositories;

public class PasswordResetRepository : IPasswordResetRepository
{
    private readonly AppDbContext _context;

    public PasswordResetRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Add(token);

        await _context.SaveChangesAsync();
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
    {
        return await _context.PasswordResetTokens
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task UpdateAsync(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Update(token);

        await _context.SaveChangesAsync();
    }
}