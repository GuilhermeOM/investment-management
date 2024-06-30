namespace InvestmentManagement.Client.API.Dtos.CompanyProduct;

using InvestmentManagement.Client.API.Dtos.Product;

public record CompanyProductRead
{
    public Guid Id { get; set; }
    public required DividendProductRead Product { get; set; }
    public double Amount { get; set; }
}
