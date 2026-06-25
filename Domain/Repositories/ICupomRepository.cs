namespace GMS_Backend.Domain.Repositories;
using GMS_Backend.Domain.Models;

public interface ICupomRepository
{
    Task Create(Cupom cupom);
    Task<IEnumerable<Cupom>> GetCupons();
    Task<Cupom?> GetByCode(string code);
}