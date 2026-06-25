using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
namespace GMS_Backend.Application.Services;

public class CupomService
{
    private readonly ICupomRepository _repository;
    private readonly IUserCupomRepository _userCupomRepository;

    public CupomService(ICupomRepository repository, IUserCupomRepository userCupomRepository)
    {
        _repository = repository;
        _userCupomRepository = userCupomRepository;
    }

    public async Task<Cupom> Create(Cupom cupom)
    {
        await _repository.Create(cupom);

        return cupom;
    }

    public async Task<IEnumerable<Cupom>> GetCupons()
    {
        return await _repository.GetCupons();
    }

    public async Task<Cupom?> Validate(string code, Guid userId)
    {
        var cupom = await _repository.GetByCode(code);

        if (cupom == null)
            return null;

        if (cupom.ExpiresAt < DateTime.UtcNow)
            return null;

        if (cupom.CurrentUsage >= cupom.MaxUsage)
            return null;

        var alreadyUsed = await _userCupomRepository.HasUserUsed(userId, cupom.Id);

        if (alreadyUsed)
            return null;

        return cupom;
    }
}