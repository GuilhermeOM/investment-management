namespace InvestmentManagement.SharedDataContext.Models;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Product
{
    public Guid Id { get; private set; }
    public Guid TypeId { get; private set; }
    public Guid CompanyId { get; private set; }

    public required ProductType Type { get; set; }
    public required Company Company { get; set; }

    public string Ticker { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double InitialPrice { get; set; }
    public DateOnly PaymentDate { get; set; }
}
