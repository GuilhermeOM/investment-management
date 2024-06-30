namespace InvestmentManagement.Client.API.Profiles;

using AutoMapper;
using InvestmentManagement.Client.API.Dtos.Client;
using InvestmentManagement.SharedDataContext.Models;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        _ = this.CreateMap<Client, ClientRead>();
        _ = this.CreateMap<Client, ClientMarketRead>();
        _ = this.CreateMap<ClientCreate, Client>();
    }
}
