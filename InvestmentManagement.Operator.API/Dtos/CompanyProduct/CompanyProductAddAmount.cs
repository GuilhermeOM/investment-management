namespace InvestmentManagement.Operator.API.Dtos.CompanyProduct;

public class CompanyProductAddAmount
{
    private string creatorEmail = string.Empty;

    public required string CreatorEmail
    {
        get => this.creatorEmail;
        set => this.creatorEmail = IsValidEmail(value) ? value : string.Empty;
    }

    public required string Ticker { get; set; }
    public required int Amount { get; set; }

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
