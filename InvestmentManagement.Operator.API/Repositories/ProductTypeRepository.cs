namespace InvestmentManagement.Operator.API.Repositories;

using System;
using System.Threading.Tasks;
using InvestmentManagement.SharedDataContext;
using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class ProductTypeRepository(DataContext context) : IProductTypeRepository
{
    private readonly DataContext context = context;

    public async Task<ProductType?> ReadProductTypeByType(string type) => await this.context.ProductTypes
        .Where(productType => productType.Type == type)
        .FirstOrDefaultAsync();

    public async Task<ProductType?> ReadProductTypeById(Guid id) => await this.context.ProductTypes
        .Where(productType => productType.Id == id)
        .FirstOrDefaultAsync();

    public async Task Create(ProductType productType)
    {
        ArgumentNullException.ThrowIfNull(productType);

        _ = await this.context.AddAsync(productType);
    }

    public async Task SaveChanges() => await this.context.SaveChangesAsync();
}
