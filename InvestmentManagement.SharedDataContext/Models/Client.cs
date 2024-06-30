namespace InvestmentManagement.SharedDataContext.Models;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Client
{
    public Guid Id { get; private set; }
    public required string Email { get; set; }
    public required double Wallet { get; set; }
    public List<ClientProduct> Products { get; set; } = [];
}
