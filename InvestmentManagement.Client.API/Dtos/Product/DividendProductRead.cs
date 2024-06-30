namespace InvestmentManagement.Client.API.Dtos.Product;

public record DividendProductRead
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public string Ticker { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double InitialPrice { get; set; }
    public DateOnly PaymentDate { get; set; }
}
