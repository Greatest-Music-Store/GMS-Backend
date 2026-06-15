using GMS_Backend.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace GMS_Backend.Infrastructure.Security;

public class PasswordHasher
{
    private readonly IPasswordHasher<User> _hasher;
    public PasswordHasher()
    {
        _hasher = new PasswordHasher<User>();
    }

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(
        User user,
        string password,
        string hash)
    {
        return _hasher.VerifyHashedPassword(
            user,
            hash,
            password
        ) != PasswordVerificationResult.Failed;
    }
}