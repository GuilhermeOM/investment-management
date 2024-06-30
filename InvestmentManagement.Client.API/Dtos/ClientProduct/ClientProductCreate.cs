namespace InvestmentManagement.Client.API.Dtos.ClientProduct;

using InvestmentManagement.SharedDataContext.Models;

public class ClientProductCreate
{
    public required Product Product { get; set; }
    public required int Amount { get; set; }
    public bool Purchasable { get; set; }
}
