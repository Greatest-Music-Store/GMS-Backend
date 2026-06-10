namespace GMS_Backend.Services.Interfaces;

using GMS_Backend.DTOs.User;

public interface IUserService
{
    Task<UserResponseDTO> CreateAsync(UserCreationDTO dto);
    Task<UserResponseDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserResponseDTO>> GetAllAsync();
    Task DeleteAsync(Guid id);
}