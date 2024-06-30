namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class ClientRepository(DataContext context) : IClientRepository
{
    private readonly DataContext context = context;

    public async Task<Client?> ReadClientById(Guid id) => await this.context.Clients
        .Where(client => client.Id == id)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Type)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Company)
        .FirstOrDefaultAsync();

    public async Task<Client?> ReadClientByEmail(string email) => await this.context.Clients
        .Where(client => client.Email == email)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Type)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Company)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<Client>> ReadPurshasableClient()
    {
        var clients = await this.context.Clients
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Type)
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Company)
            .ToListAsync();

        foreach (var client in clients)
        {
            client.Products = client.Products
                .Where(cp => cp.Purchasable)
                .ToList();
        }

        return clients.Where(client => client.Products.Count > 0);
    }

    public async Task<Client?> ReadPurshasableClientByEmail(string email)
    {
        var clients = await this.context.Clients
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Type)
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Company)
            .ToListAsync();

        var client = clients.FirstOrDefault(client => client.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (client != null)
        {
            var purshasableProducts = client.Products.Where(cp => cp.Purchasable).ToList();
            if (purshasableProducts.Count == 0)
            {
                return null;
            }

            client.Products = purshasableProducts;
        }

        return client;
    }

    public async Task<IEnumerable<Client>> ReadPurshasableClientsByTicker(string ticker)
    {
        var clients = await this.context.Clients
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Type)
            .Include(company => company.Products)
            .ThenInclude(product => product.Product)
            .ThenInclude(product => product.Company)
            .ToListAsync();

        foreach (var client in clients)
        {
            client.Products = client.Products
                .Where(cp => cp.Product.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase) && cp.Purchasable)
                .ToList();
        }

        return clients.Where(client => client.Products.Count > 0);
    }

    public async Task Create(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);

        _ = await this.context.AddAsync(client);
    }

    public async Task AddClientProduct(string email, ClientProduct clientProduct)
    {
        ArgumentNullException.ThrowIfNull(clientProduct);

        var client = await this.context.Clients
            .Where(client => client.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            .FirstAsync();

        client.Products.Add(clientProduct);
    }

    public async Task AddClientProductAmount(Guid id, int amount) => _ = await this.context.ClientProducts
        .Where(clientProduct => clientProduct.Id == id)
        .ExecuteUpdateAsync(property => property.SetProperty(p => p.Amount, a => a.Amount + amount));

    public async Task SubtractClientProductAmount(Guid id, int amount) => _ = await this.context.ClientProducts
        .Where(clientProduct => clientProduct.Id == id)
        .ExecuteUpdateAsync(property => property.SetProperty(p => p.Amount, a => a.Amount - amount));

    public async Task SumWallet(Guid id, double amount)
    {
        var client = await this.context.Clients.Where(client => client.Id == id).FirstAsync();
        client.Wallet += amount;
    }

    public async Task SubtractWallet(Guid id, double amount)
    {
        var client = await this.context.Clients.Where(client => client.Id == id).FirstAsync();
        client.Wallet -= amount;
    }

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
