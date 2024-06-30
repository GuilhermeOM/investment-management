namespace InvestmentManagement.Operator.API.Dtos.Product;

using InvestmentManagement.SharedDataContext.Models.Enums;

public record DividendProductCreate
{
    private string creatorEmail = string.Empty;

    public string TypeName { get; } = Enum.GetName(FinancialProductType.DIVIDEND)!;

    public required string CreatorEmail
    {
        get => this.creatorEmail;
        set => this.creatorEmail = IsValidEmail(value) ? value : string.Empty;
    }

    public required string Ticker { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double InitialPrice { get; set; }
    public required DateOnly PaymentDate { get; set; }

    private static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith('.'))
        {
            return false;
        }

        try
        {
            var mail = new System.Net.Mail.MailAddress(email);
            return mail.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
