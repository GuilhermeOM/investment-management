namespace InvestmentManagement.Operator.API.Profiles;

using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.CompanyProduct;
using InvestmentManagement.Operator.API.Dtos.Product;
using InvestmentManagement.SharedDataContext.Models;

public class CompanyProductProfile : Profile
{
    public CompanyProductProfile()
    {
        _ = this.CreateMap<CompanyProduct, CompanyProductRead>();
        _ = this.CreateMap<CompanyProductCreate, CompanyProduct>();
        _ = this.CreateMap<CompanyProduct, DividendProductRead>()
            .ForMember(dest => dest.Creator, src => src.MapFrom(product => product.Product.Company.Email))
            .ForMember(dest => dest.Type, src => src.MapFrom(product => product.Product.Type.Type))
            .ForMember(dest => dest.Description, src => src.MapFrom(product => product.Product.Description))
            .ForMember(dest => dest.PaymentDate, src => src.MapFrom(product => product.Product.PaymentDate))
            .ForMember(dest => dest.InitialPrice, src => src.MapFrom(product => product.Product.InitialPrice))
            .ForMember(dest => dest.Ticker, src => src.MapFrom(product => product.Product.Ticker));
    }
}
