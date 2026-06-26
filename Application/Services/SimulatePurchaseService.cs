using GMS_Backend.Application.Results;
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
namespace GMS_Backend.Application.Services;

public class SimulatePurchaseService
{
    private readonly ICupomRepository _cupomRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserCupomRepository _userCupomRepository;
    private readonly CupomService _cupomService;


    public SimulatePurchaseService(ICupomRepository cupomRepository, IUserRepository userRepository, IProductRepository productRepository, IUserCupomRepository userCupomRepository, CupomService cupomService)
    {
        _cupomRepository = cupomRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _userCupomRepository = userCupomRepository;
        _cupomService = cupomService;
    }

    public async Task<PurchaseResult> SimulatePurchase(Guid userId, ICollection<Guid> productIds, string? cupomCode)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new Exception("Usuário não encontrado.");

        foreach (var productId in productIds)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Produto não encontrado.");

            if (product.Quantity <= 0)
                throw new Exception($"O produto '{product.Name}' está indisponível.");

            product.Quantity--;

            user.PurchasedProducts.Add(product);

            await _productRepository.Update(product);
        }    

        if (!string.IsNullOrWhiteSpace(cupomCode))
        {
            var cupom = await _cupomService.Validate(cupomCode, userId);

            if (cupom == null)
                return PurchaseResult.Fail("Cupom inválido.");

            cupom.CurrentUsage++;

            await _cupomRepository.Update(cupom);

            await _userCupomRepository.Add(new UserCupom
            {
                UserId = user.Id,
                CupomId = cupom.Id
            });

            return PurchaseResult.Ok(true, cupom.PercentualValue);
        }
        await _userRepository.UpdateAsync(user);    

        return PurchaseResult.Ok();
    }
}