using Microsoft.EntityFrameworkCore;
using GMS_Backend.Data;
using GMS_Backend.DTOs.Feedback;
using GMS_Backend.Mappers;
using GMS_Backend.Models;
using GMS_Backend.Services.Interfaces;

namespace GMS_Backend.Services;

public class FeedbackService : IFeedbackService
{
    private readonly AppDbContext _context;

    public FeedbackService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FeedbackResponseDTO> CreateAsync(
        FeedbackCreationDTO dto, Guid userId)
    {
        var feedback = FeedbackMapper.ToModel(dto, userId);

        _context.Feedbacks.Add(feedback);

        await _context.SaveChangesAsync();

        return FeedbackMapper.ToDto(feedback);
    }

    public async Task<FeedbackResponseDTO?> GetByIdAsync(Guid id)
    {
        var feedback = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Product)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (feedback == null)
            return null;

        return FeedbackMapper.ToDto(feedback);
    }

    public async Task<IEnumerable<FeedbackResponseDTO>> GetAllAsync()
    {
        var feedbacks = await _context.Feedbacks
            .Include(f => f.User)
            .Include(f => f.Product)
            .ToListAsync();

        return feedbacks
            .Select(FeedbackMapper.ToDto)
            .ToList();
    }

    public async Task DeleteAsync(Guid id)
    {
        var feedback = await _context.Feedbacks
            .FirstOrDefaultAsync(f => f.Id == id);

        if (feedback == null)
            throw new KeyNotFoundException("Feedback não encontrado.");

        _context.Feedbacks.Remove(feedback);

        await _context.SaveChangesAsync();
    }
}