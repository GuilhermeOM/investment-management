namespace InvestmentManagement.Client.API.Dtos.ClientProduct;

public class ClientProductSell
{
    private string clientEmail = string.Empty;

    public required string ClientEmail
    {
        get => this.clientEmail;
        set => this.clientEmail = IsValidEmail(value) ? value : string.Empty;
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
