namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.SharedDataContext.Models;

public interface IClientProductRepository
{
    Task<ClientProduct?> ReadById(Guid id);
    Task<ClientProduct?> ReadByTicker(string ticker);
    Task<Guid> Create(ClientProduct clientProduct);
    Task SaveChanges();
}
