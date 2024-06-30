namespace InvestmentManagement.Client.API.Dtos.Client;

public class ClientDeposit
{
    private string clientEmail = string.Empty;

    public required string ClientEmail
    {
        get => this.clientEmail;
        set => this.clientEmail = IsValidEmail(value) ? value : string.Empty;
    }

    public required double Amount { get; set; }

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
