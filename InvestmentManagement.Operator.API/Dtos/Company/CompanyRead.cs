namespace InvestmentManagement.Operator.API.Dtos.Company;

using InvestmentManagement.Operator.API.Dtos.CompanyProduct;

public record CompanyRead
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required List<CompanyProductRead> Products { get; set; }
}
