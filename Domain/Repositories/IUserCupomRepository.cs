using GMS_Backend.Domain.Models;

namespace GMS_Backend.Domain.Repositories;

public interface IUserCupomRepository
{
    Task<bool> HasUserUsed(Guid userId, Guid cupomId);
    Task Add(UserCupom userCupom);
}