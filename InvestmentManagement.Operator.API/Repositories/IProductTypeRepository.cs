namespace InvestmentManagement.Operator.API.Repositories;

using InvestmentManagement.SharedDataContext.Models;

public interface IProductTypeRepository
{
    Task<ProductType?> ReadProductTypeByType(string type);
    Task<ProductType?> ReadProductTypeById(Guid id);
    Task Create(ProductType productType);
    Task SaveChanges();
}
