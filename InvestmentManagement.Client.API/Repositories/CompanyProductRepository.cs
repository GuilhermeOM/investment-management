namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.Client.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class CompanyProductRepository(DataContext context) : ICompanyProductRepository
{
    private readonly DataContext context = context;

    public async Task<PagedList<CompanyProduct>> ReadCompanyProductWithPagging(int page, int pageSize) => await PagedList<CompanyProduct>
        .CreateAsync(this.context.CompanyProducts
            .Include(company => company.Product)
            .Include(company => company.Product.Type)
            .Include(company => company.Product.Company), page, pageSize);

    public async Task<PagedList<CompanyProduct>> ReadCompanyProductByEmailWithPagging(string email, int page, int pageSize) => await PagedList<CompanyProduct>
        .CreateAsync(this.context.CompanyProducts
            .Where(company => company.Product.Company.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
            .Include(company => company.Product)
            .Include(company => company.Product.Type)
            .Include(company => company.Product.Company), page, pageSize);

    public async Task<CompanyProduct?> ReadCompanyProductByTicker(string ticker) => await this.context.CompanyProducts
        .Where(company => company.Product.Ticker.Equals(ticker, StringComparison.OrdinalIgnoreCase))
        .Include(company => company.Product)
        .Include(company => company.Product.Type)
        .Include(company => company.Product.Company)
        .FirstOrDefaultAsync();

    public async Task BuyCompanyProduct(Guid id, int amount)
    {
        var companyProduct = await this.context.CompanyProducts
            .Where(companyProduct => companyProduct.Product.Id == id)
            .FirstAsync();

        companyProduct.Amount -= amount;
    }

    public void Delete(CompanyProduct companyProduct)
    {
        ArgumentNullException.ThrowIfNull(companyProduct);

        _ = this.context.CompanyProducts.Attach(companyProduct);
        _ = this.context.CompanyProducts.Remove(companyProduct);
    }

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
