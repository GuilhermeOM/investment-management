namespace InvestmentManagement.SharedDataContext.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ClientId { get; set; }

    public required string Type { get; set; }
    public required double Cost { get; set; }
    public required Product Product { get; set; }
    public required Client Client { get; set; }
    public DateTime Moment { get; set; }
}
