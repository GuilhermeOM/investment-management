namespace InvestmentManagement.UI.Entities;

using InvestmentManagement.UI.Entities.Enums;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
}
