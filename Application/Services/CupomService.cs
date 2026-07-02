using GMS_Backend.Application.Results;
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

    public async Task<CupomValidationResult> Validate(string code, Guid userId)
    {
        var cupom = await _repository.GetByCode(code);

        if (cupom == null)
            return new CupomValidationResult
            {
                Success = false,
                Message = "Cupom não encontrado."
            };

        if (cupom.ExpiresAt < DateTime.UtcNow)
            return new CupomValidationResult
            {
                Success = false,
                Message = "Cupom expirado."
            };

        if (cupom.CurrentUsage >= cupom.MaxUsage)
            return new CupomValidationResult
            {
                Success = false,
                Message = "Cupom esgotado."
            };

        var alreadyUsed = await _userCupomRepository.HasUserUsed(userId, cupom.Id);

        if (alreadyUsed)
            return new CupomValidationResult
            {
                Success = false,
                Message = "Você já utilizou este cupom."
            };

        return new CupomValidationResult
        {
            Success = true,
            Message = "Cupom validado",
            Cupom = cupom
        };
    }
}