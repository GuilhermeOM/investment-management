namespace InvestmentManagement.Operator.API.Profiles;

using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.ProductType;
using InvestmentManagement.SharedDataContext.Models;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        _ = this.CreateMap<ProductType, ProductTypeRead>();
        _ = this.CreateMap<ProductTypeCreate, ProductType>();
    }
}
