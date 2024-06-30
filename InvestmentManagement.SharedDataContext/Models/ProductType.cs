namespace InvestmentManagement.SharedDataContext.Models;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class ProductType
{
    public Guid Id { get; private set; }
    public required string Type { get; set; }
}
