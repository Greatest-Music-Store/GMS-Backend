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

    public async Task<PurchaseResult> SimulatePurchase(Guid userId, string? cupomCode)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return PurchaseResult.Fail("Usuário não encontrado.");
        }
        
        if (!string.IsNullOrWhiteSpace(cupomCode))
        {
            var result = await _cupomService.Validate(cupomCode, userId);

            var cupom = result.Cupom!;

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
        
        foreach (var cartItem in user.CartItems.ToList())
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

            if (product == null)
                return PurchaseResult.Fail("Produto não encontrado.");

            if (product.Quantity < cartItem.Quantity)
                return PurchaseResult.Fail(
                    $"Há apenas {product.Quantity} unidade(s) de '{product.Name}' em estoque.");

            product.Quantity -= cartItem.Quantity;

            user.PurchasedProducts.Add(product);

            user.CartItems.Remove(cartItem);

            await _productRepository.Update(product);
        }
        
        await _userRepository.UpdateAsync(user);    

        return PurchaseResult.Ok();
    }
}