using GMS_Backend.DTOs.Product;
using GMS_Backend.DTOs.User;
using GMS_Backend.Models;

namespace GMS_Backend.Mappers;

public static class UserMapper
{
    public static User ToModel(UserCreationDTO dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Cpf = dto.Cpf,

            //depois vai ser bcrypt
            PasswordHash = dto.Password
        };
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