namespace InvestmentManagement.SharedDataContext.Models;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Company
{
    public Guid Id { get; private set; }
    public required string Email { get; set; }
    public List<CompanyProduct> Products { get; set; } = [];
}
