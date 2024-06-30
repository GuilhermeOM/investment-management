namespace InvestmentManagement.Operator.API.Repositories;

using InvestmentManagement.SharedDataContext.Models;

public interface ICompanyRepository
{
    Task<Company?> ReadCompanyById(Guid id);
    Task<Company?> ReadCompanyByEmail(string email);
    Task Create(Company company);
    Task SaveChanges();
}
