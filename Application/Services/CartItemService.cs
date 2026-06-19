using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Repositories;
namespace GMS_Backend.Application.Services;
public class CartItemService
{
    private readonly ICartItemRepository _repository;
    private readonly IProductRepository _productRepository;

    public CartItemService(ICartItemRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<CartItem> CreateAsync(CartItem cartItem)
    {
        var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
        if (product == null)
        {
            throw new Exception("Produto não encontrado");
        }
        if (cartItem.Quantity <= 0)
        {
            throw new Exception("Quantidade inválida");
        }
        if (cartItem.Quantity > product.Quantity)
        {
            throw new Exception("Quantidade maior do que a disponível em estoque");
        }
        var existing = await _repository.GetAsync(cartItem.UserId, cartItem.ProductId);

        if (existing != null)
        {
            int newQuantity = existing.Quantity + cartItem.Quantity;
            if (newQuantity > product.Quantity)
            {
                throw new Exception("Quantidade maior do que a disponível em estoque");
            }

            existing.Quantity = newQuantity;

            await _repository.UpdateAsync(existing);

            return existing;
        }

        await _repository.CreateAsync(cartItem);

        return await _repository.GetAsync(cartItem.UserId, cartItem.ProductId) ?? throw new Exception("Erro ao carregar item criado");
    }

    public async Task<CartItem> DeleteAsync(CartItem cartItem)
    {
        await _repository.DeleteAsync(cartItem);

        return cartItem;
    }

    public async Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId)
    {
        return await _repository.GetByUserIdAsync(userId);
    }

    public async Task<CartItem?> GetAsync(Guid userId, Guid productId)
    {
        return await _repository.GetAsync(userId, productId);
    }


}