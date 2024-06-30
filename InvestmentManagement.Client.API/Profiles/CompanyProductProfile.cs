namespace InvestmentManagement.Client.API.Profiles;

using AutoMapper;
using InvestmentManagement.Client.API.Dtos.CompanyProduct;
using InvestmentManagement.SharedDataContext.Models;

public class CompanyProductProfile : Profile
{
    public CompanyProductProfile() => _ = this.CreateMap<CompanyProduct, CompanyProductRead>();
}
