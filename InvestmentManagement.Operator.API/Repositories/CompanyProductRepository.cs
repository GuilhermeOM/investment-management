namespace InvestmentManagement.Operator.API.Repositories;

using InvestmentManagement.Operator.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class CompanyProductRepository(DataContext context) : ICompanyProductRepository
{
    private readonly DataContext context = context;

    public async Task<CompanyProduct?> ReadCompanyProductById(Guid id) => await this.context.CompanyProducts
        .Where(company => company.Id == id)
        .Include(company => company.Product)
        .Include(company => company.Product.Type)
        .Include(company => company.Product.Company)
        .FirstOrDefaultAsync();

    public async Task<IEnumerable<CompanyProduct>> ReadCompanyProductByEmail(string email) => await this.context.CompanyProducts
        .Where(companyProduct => companyProduct.Product.Company.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
        .Include(company => company.Product)
        .Include(company => company.Product.Type)
        .Include(company => company.Product.Company)
        .ToListAsync();

    public async Task<PagedList<CompanyProduct>> ReadCompanyProductByEmailWithPagging(string email, int page, int pageSize) => await PagedList<CompanyProduct>
        .CreateAsync(this.context.CompanyProducts
            .Where(companyProduct => companyProduct.Product.Company.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            .Include(company => company.Product)
            .Include(company => company.Product.Type)
            .Include(company => company.Product.Company), page, pageSize);

    public async Task<CompanyProduct?> ReadCompanyProductByTicker(string ticker) => _ = await this.context.CompanyProducts
        .Include(company => company.Product)
        .ThenInclude(product => product.Ticker == ticker)
        .Include(company => company.Product.Type)
        .Include(company => company.Product.Company)
        .FirstOrDefaultAsync();

    public async Task Create(CompanyProduct companyProduct)
    {
        ArgumentNullException.ThrowIfNull(companyProduct);

        _ = await this.context.AddAsync(companyProduct);
    }

    public async Task<int> SumCompanyProductAmount(string ticker, int amount) => await this.context.CompanyProducts
        .Where(companyProduct => companyProduct.Product.Ticker == ticker)
        .ExecuteUpdateAsync(property =>
            property.SetProperty(property => property.Amount, product => product.Amount + amount));

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
