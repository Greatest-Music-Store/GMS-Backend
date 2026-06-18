using GMS_Backend.Api.DTOs.User;
using GMS_Backend.Domain.Models;

namespace GMS_Backend.Api.Mappers;

public static class UserMapper
{
    public static User ToModel(UserCreationDTO dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email.Trim().ToLowerInvariant(),
            Phone = dto.Phone,
            Cpf = dto.Cpf,
        };
    }

    public static void UpdateToModel(User user, UserUpdateDTO dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Name))
        user.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email.Trim().ToLowerInvariant();

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            user.Phone = dto.Phone;

        if (dto.Cep != null)
            user.Cep = dto.Cep;
    }

    public static UserResponseDTO ToDto(User user)
    {
        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Cep = user.Cep,

            Favorites = user.Favorites
                .Select(f => ProductMapper.ToDto(f.Product))
                .ToList(),

            Cart = user.CartItems
                .Select(c => ProductMapper.ToDto(c.Product))
                .ToList()
        };
    }
}