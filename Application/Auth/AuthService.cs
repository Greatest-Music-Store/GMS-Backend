
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
using GMS_Backend.Infrastructure.Security;
namespace GMS_Backend.Application.Auth;
public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly EmailService _emailService;
    private readonly IPasswordResetRepository _passwordResetRepository;
    private readonly JwtService _jwtService;

    public AuthService(IUserRepository userRepository, PasswordHasher passwordHasher, JwtService jwtService, EmailService emailService, IPasswordResetRepository passwordResetRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
        _passwordResetRepository = passwordResetRepository;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        var existingUser = await _userRepository.GetByEmailAsync(user.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "Email já cadastrado");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        await _userRepository.CreateAsync(user);

        return user;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

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

    public async Task ForgotPassword(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return;

        var token = Guid.NewGuid().ToString();

        Random gerador = new();
        int[] codes = new int[4];
        for (int i = 0; i < codes.Length; i++)
        {
            codes[i] = gerador.Next(1, 10);
        }

        await _passwordResetRepository.CreateAsync(
            new PasswordResetToken{
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                Used = false
            });

        var link = $"http://localhost:4200/reset-password?token={token}";

        await _emailService.SendResetPasswordEmail(email, link, codes);
    }

    public async Task ResetPassword(string token, string newPassword)
    {
        var resetToken = await _passwordResetRepository.GetByTokenAsync(token);

        if (resetToken == null)
            throw new Exception(
                "Token inválido");

        if (resetToken.Used)
            throw new Exception(
                "Token já utilizado");

        if (resetToken.ExpiresAt <
            DateTime.UtcNow)
            throw new Exception(
                "Token expirado");

        var user = await _userRepository.GetByIdAsync(resetToken.UserId);

        if (user == null)
            throw new Exception(
                "Usuário não encontrado");

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

        await _userRepository.UpdateAsync(user);

        resetToken.Used = true;

        await _passwordResetRepository.UpdateAsync(resetToken);
    }
}