namespace InvestmentManagement.SharedDataContext.Models;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class CompanyProduct
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public required Product Product { get; set; }
    public required int Amount { get; set; }
}
