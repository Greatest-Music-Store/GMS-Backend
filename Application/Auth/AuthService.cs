
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

    public async Task<bool> ForgotPassword(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null) return false;

        Random gerador = new();
        string code = gerador.Next(1, 10000).ToString("D4");
        
        await _passwordResetRepository.CreateAsync(
            new PasswordResetToken{
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = code,
                ExpiresAt = DateTime.UtcNow.AddHours(0.5),
                Used = false
            });

        await _emailService.SendResetPasswordEmail(email, code);
        return true;
    }

    public async Task ResetPassword(string token, string newPassword)
    {
        var resetToken = await _passwordResetRepository.GetByTokenAsync(token);

        if (resetToken == null)
            throw new KeyNotFoundException(
                "Código inválido");

        if (resetToken.Used)
            throw new InvalidOperationException(
                "Código já utilizado");

        if (resetToken.ExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException(
                "Código expirado");

        var user = await _userRepository.GetByIdAsync(resetToken.UserId);

        if (user == null) throw new KeyNotFoundException("Usuário não encontrado");

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

        await _userRepository.UpdateAsync(user);

        resetToken.Used = true;

        await _passwordResetRepository.UpdateAsync(resetToken);
    }
}