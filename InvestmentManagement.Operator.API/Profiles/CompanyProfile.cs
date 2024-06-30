namespace InvestmentManagement.Operator.API.Profiles;

using AutoMapper;
using InvestmentManagement.Operator.API.Dtos.Company;
using InvestmentManagement.SharedDataContext.Models;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        _ = this.CreateMap<Company, CompanyRead>();
        _ = this.CreateMap<CompanyCreate, Company>();
    }
}
