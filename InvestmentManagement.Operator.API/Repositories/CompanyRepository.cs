namespace InvestmentManagement.Operator.API.Repositories;

using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class CompanyRepository(DataContext context) : ICompanyRepository
{
    private readonly DataContext context = context;

    public async Task<Company?> ReadCompanyById(Guid id) => await this.context.Companies
        .Where(company => company.Id == id)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Type)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Company)
        .FirstOrDefaultAsync();

    public async Task<Company?> ReadCompanyByEmail(string email) => await this.context.Companies
        .Where(company => company.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Type)
        .Include(company => company.Products)
        .ThenInclude(product => product.Product)
        .ThenInclude(product => product.Company)
        .FirstOrDefaultAsync();

    public async Task Create(Company company)
    {
        ArgumentNullException.ThrowIfNull(company);

        _ = await this.context.AddAsync(company);
    }

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
