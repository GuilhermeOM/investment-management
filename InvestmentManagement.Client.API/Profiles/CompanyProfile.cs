namespace InvestmentManagement.Client.API.Profiles;

using AutoMapper;
using InvestmentManagement.Client.API.Dtos.Company;
using InvestmentManagement.SharedDataContext.Models;

public class CompanyProfile : Profile
{
    public CompanyProfile() => _ = this.CreateMap<Company, CompanyRead>();
}
