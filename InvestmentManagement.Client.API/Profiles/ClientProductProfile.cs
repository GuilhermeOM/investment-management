namespace InvestmentManagement.Client.API.Profiles;

using AutoMapper;
using InvestmentManagement.Client.API.Dtos.ClientProduct;
using InvestmentManagement.Client.API.Dtos.Product;
using InvestmentManagement.SharedDataContext.Models;

public class ClientProductProfile : Profile
{
    public ClientProductProfile()
    {
        _ = this.CreateMap<ClientProduct, ClientProductRead>();
        _ = this.CreateMap<ClientProductCreate, ClientProduct>();
        _ = this.CreateMap<ClientProduct, DividendProductRead>()
            .ForMember(dest => dest.Creator, src => src.MapFrom(product => product.Product.Company.Email))
            .ForMember(dest => dest.Type, src => src.MapFrom(product => product.Product.Type.Type))
            .ForMember(dest => dest.Description, src => src.MapFrom(product => product.Product.Description))
            .ForMember(dest => dest.PaymentDate, src => src.MapFrom(product => product.Product.PaymentDate))
            .ForMember(dest => dest.InitialPrice, src => src.MapFrom(product => product.Product.InitialPrice))
            .ForMember(dest => dest.Ticker, src => src.MapFrom(product => product.Product.Ticker));
    }
}
