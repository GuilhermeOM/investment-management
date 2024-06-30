namespace InvestmentManagement.Client.API.Repositories;

using InvestmentManagement.Client.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext.Models;

public interface ICompanyProductRepository
{
    Task<PagedList<CompanyProduct>> ReadCompanyProductWithPagging(int page, int pageSize);
    Task<PagedList<CompanyProduct>> ReadCompanyProductByEmailWithPagging(string email, int page, int pageSize);
    Task<CompanyProduct?> ReadCompanyProductByTicker(string ticker);
    Task BuyCompanyProduct(Guid id, int amount);
    void Delete(CompanyProduct companyProduct);
    Task SaveChanges();
}
