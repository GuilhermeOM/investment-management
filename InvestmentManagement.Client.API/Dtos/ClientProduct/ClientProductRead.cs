namespace InvestmentManagement.Client.API.Dtos.ClientProduct;

using InvestmentManagement.Client.API.Dtos.Product;

public class ClientProductRead
{
    public Guid Id { get; set; }
    public required DividendProductRead Product { get; set; }
    public required int Amount { get; set; }
    public required bool Purchasable { get; set; }
}
