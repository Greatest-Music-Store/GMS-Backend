using GMS_Backend.Domain.Models;

namespace GMS_Backend.Domain.Repositories;
public interface IPasswordResetRepository
{
    Task CreateAsync(PasswordResetToken token);

    Task<PasswordResetToken?> GetByTokenAsync(
        string token);

    Task UpdateAsync(
        PasswordResetToken token);
}