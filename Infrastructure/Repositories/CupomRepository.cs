namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CupomRepository : ICupomRepository
{
    private readonly AppDbContext _context;

    public CupomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(Cupom cupom)
    {
        _context.Cupons.Add(cupom);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cupom>> GetCupons()
    {
        return await _context.Cupons.ToListAsync();
    }

    public async Task<Cupom?> GetByCode(string code)
    {
        return await _context.Cupons.FirstOrDefaultAsync(c => c.Code == code);
    }
}