using GMS_Backend.Api.DTOs.Product;

namespace GMS_Backend.Api.DTOs.Purchase;
public class PurchasedProductsDTO
{
    public required IEnumerable<ProductResponseDTO> PurchasedProducts { get; set; }
}