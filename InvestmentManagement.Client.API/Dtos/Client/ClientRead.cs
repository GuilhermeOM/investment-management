namespace InvestmentManagement.Client.API.Dtos.Client;

using InvestmentManagement.Client.API.Dtos.ClientProduct;

public record ClientRead
{
    public Guid Id { get; private set; }
    public string Email { get; set; } = string.Empty;
    public List<ClientProductRead> Products { get; set; } = [];
    public double Wallet { get; set; }
}
