namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UserCupomRepository : IUserCupomRepository
{
    private readonly AppDbContext _context;

    public UserCupomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Add(UserCupom userCupom)
    {
        await _context.UsersCupons.AddAsync(userCupom);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasUserUsed(Guid userId, Guid cupomId)
    {
        var userCupom = await _context.UsersCupons.FirstOrDefaultAsync(p => p.UserId == userId && p.CupomId == cupomId);
        if (userCupom == null)
        {
            return false;
        } else return true;
    }
}