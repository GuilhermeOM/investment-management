namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class ClientProductRepository(DataContext context) : IClientProductRepository
{
    private readonly DataContext context = context;

    public async Task<ClientProduct?> ReadById(Guid id) => await this.context.ClientProducts
        .Where(client => client.Id == id)
        .Include(client => client.Product)
        .Include(client => client.Product.Type)
        .Include(client => client.Product.Company)
        .FirstOrDefaultAsync();

    public async Task<ClientProduct?> ReadByTicker(string ticker) => await this.context.ClientProducts
        .Where(client => client.Product.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase))
        .Include(client => client.Product)
        .Include(client => client.Product.Type)
        .Include(client => client.Product.Company)
        .FirstOrDefaultAsync();

    public async Task<ClientProduct?> ReadPurshasableByTicker(string ticker)
    {
        var clientProducts = await this.context.ClientProducts
            .Where(client => client.Product.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase))
            .Include(client => client.Product)
            .Include(client => client.Product.Type)
            .Include(client => client.Product.Company)
            .ToListAsync();

        return clientProducts.FirstOrDefault(cp => !cp.Purchasable);
    }

    public async Task<Guid> Create(ClientProduct clientProduct)
    {
        ArgumentNullException.ThrowIfNull(clientProduct);

        _ = await this.context.AddAsync(clientProduct);
        _ = await this.context.SaveChangesAsync();

        return clientProduct.Id;
    }

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
