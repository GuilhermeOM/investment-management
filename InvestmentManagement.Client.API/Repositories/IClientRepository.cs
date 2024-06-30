namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.SharedDataContext.Models;

public interface IClientRepository
{
    Task<Client?> ReadClientById(Guid id);
    Task<Client?> ReadClientByEmail(string email);
    Task<IEnumerable<Client>> ReadPurshasableClient();
    Task<Client?> ReadPurshasableClientByEmail(string email);
    Task<IEnumerable<Client>> ReadPurshasableClientsByTicker(string ticker);
    Task Create(Client client);
    Task AddClientProduct(string email, ClientProduct clientProduct);
    Task AddClientProductAmount(Guid id, int amount);
    Task SumWallet(Guid id, double amount);
    Task SubtractClientProductAmount(Guid id, int amount);
    Task SubtractWallet(Guid id, double amount);
    Task SaveChanges();
}
