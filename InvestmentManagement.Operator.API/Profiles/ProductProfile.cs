namespace InvestmentManagement.Operator.API.Profiles;

using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.Product;
using InvestmentManagement.SharedDataContext.Models;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        _ = this.CreateMap<Product, DividendProductRead>()
            .ForMember(dest => dest.Type, src => src.MapFrom(product => product.Type.Type))
            .ForMember(dest => dest.Creator, src => src.MapFrom(product => product.Company.Email));
        _ = this.CreateMap<DividendProductCreate, Product>();
    }
}
