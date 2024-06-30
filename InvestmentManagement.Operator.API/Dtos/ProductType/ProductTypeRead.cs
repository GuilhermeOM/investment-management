namespace InvestmentManagement.Operator.API.Dtos.ProductType;

using System.Globalization;
using InvestmentManagement.SharedDataContext.Models.Enums;

public record ProductTypeRead
{
    private string type = string.Empty;

    public Guid Id { get; set; }
    public required string Type
    {
        get => this.GetProductType();
        set => this.type = value;
    }

    private string GetProductType()
    {
        var typeName = Enum.GetNames(typeof(FinancialProductType))
            .Where(tName => tName.Equals(this.type, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault("");

        if (string.IsNullOrEmpty(typeName))
        {
            return Enum.GetName(FinancialProductType.UNDEFINED)!;
        }

        return typeName.ToUpper(CultureInfo.CurrentCulture)!;
    }
}
