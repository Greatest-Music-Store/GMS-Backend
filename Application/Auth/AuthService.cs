
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Security;
namespace GMS_Backend.Application.Auth;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        var existingUser =
            await _userRepository.GetByEmailAsync(user.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Email já cadastrado");
        }

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                password);

        await _userRepository.CreateAsync(user);

        return user;
    }
}