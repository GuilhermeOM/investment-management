namespace InvestmentManagement.Operator.API.Repositories;

using InvestmentManagement.Operator.API.Repositories.Helper;
using InvestmentManagement.SharedDataContext.Models;

public interface ICompanyProductRepository
{
    Task<CompanyProduct?> ReadCompanyProductById(Guid id);
    Task<IEnumerable<CompanyProduct>> ReadCompanyProductByEmail(string email);
    Task<PagedList<CompanyProduct>> ReadCompanyProductByEmailWithPagging(string email, int page, int pageSize);
    Task<CompanyProduct?> ReadCompanyProductByTicker(string ticker);
    Task Create(CompanyProduct companyProduct);
    Task<int> SumCompanyProductAmount(string ticker, int amount);
    Task SaveChanges();
}
