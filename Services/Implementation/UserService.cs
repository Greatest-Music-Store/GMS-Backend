using GMS_Backend.Data;
using GMS_Backend.DTOs.User;
using GMS_Backend.Mappers;
using GMS_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GMS_Backend.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponseDTO> CreateAsync(UserCreationDTO dto)
    {
        var user = UserMapper.ToModel(dto);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return UserMapper.ToDto(user);
    }

    public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Favorites)
                .ThenInclude(f => f.Product)
            .Include(u => u.CartItems)
                .ThenInclude(c => c.Product)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;

        return UserMapper.ToDto(user);
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.Favorites)
                .ThenInclude(f => f.Product)
            .Include(u => u.CartItems)
                .ThenInclude(c => c.Product)
            .ToListAsync();

        return users
            .Select(UserMapper.ToDto)
            .ToList();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }
}