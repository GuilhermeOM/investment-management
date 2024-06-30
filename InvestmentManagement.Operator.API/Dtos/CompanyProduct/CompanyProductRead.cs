namespace InvestmentManagement.Operator.API.Dtos.CompanyProduct;

using InvestmentManagement.Operator.API.Dtos.Product;

public record CompanyProductRead
{
    public Guid Id { get; set; }
    public required DividendProductRead Product { get; set; }
    public int Amount { get; set; }
}
