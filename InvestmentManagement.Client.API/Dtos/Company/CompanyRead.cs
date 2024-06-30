namespace InvestmentManagement.Client.API.Dtos.Company;

public record CompanyRead
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
}
