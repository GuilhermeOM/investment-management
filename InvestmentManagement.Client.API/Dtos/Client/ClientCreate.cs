namespace InvestmentManagement.Client.API.Dtos.Client;

public record ClientCreate
{
    private string email = string.Empty;
    public required string Email
    {
        get => this.email;
        set => this.email = IsValidEmail(value) ? value : string.Empty;
    }

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
