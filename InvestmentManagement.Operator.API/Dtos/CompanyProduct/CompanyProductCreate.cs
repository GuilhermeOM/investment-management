namespace InvestmentManagement.Operator.API.Dtos.CompanyProduct;

using InvestmentManagement.Operator.API.Dtos.Product;

public record CompanyProductCreate
{
    public required DividendProductCreate Product { get; set; }
    public required int Amount { get; set; }
}
