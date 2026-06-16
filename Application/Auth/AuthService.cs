
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
namespace GMS_Backend.Application.Auth;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthService(IUserRepository userRepository, PasswordHasher passwordHasher, JwtService jwtService)
    {
        _jwtService = jwtService;
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

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        await _userRepository.CreateAsync(user);

        return user;
    }

    public async Task<string> LoginAsync(
    string email,
    string password)
{
    var user =
        await _userRepository.GetByEmailAsync(email);

    if (user == null)
    {
        throw new UnauthorizedAccessException(
            "Credenciais inválidas");
    }

    var validPassword = _passwordHasher.VerifyPassword(user, password, user.PasswordHash);

    if (!validPassword)
    {
        throw new UnauthorizedAccessException(
            "Credenciais inválidas");
    }

    return _jwtService.GenerateToken(user);
}
}