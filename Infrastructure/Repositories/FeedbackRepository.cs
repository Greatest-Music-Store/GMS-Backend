namespace GMS_Backend.Infrastructure.Repositories;

using GMS_Backend.Data;

using Microsoft.EntityFrameworkCore;
using GMS_Backend.Services.Interfaces;
using GMS_Backend.Models;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly AppDbContext _context;

    public FeedbackRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Feedback feedback)
    {
        _context.Feedbacks.Add(feedback);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Feedback feedback)
    {
        _context.Feedbacks.Remove(feedback);

        await _context.SaveChangesAsync();
    }

    public async Task<Feedback?> GetByIdAsync(Guid id)
    {
        return await _context.Feedbacks
            .FirstOrDefaultAsync(
                p => p.Id == id);
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync()
    {
        return await _context.Feedbacks.ToListAsync();
    }
}