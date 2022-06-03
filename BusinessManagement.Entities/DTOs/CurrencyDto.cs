namespace BusinessManagement.Entities.DTOs;

public record CurrencyDto
{
    public byte CurrencyId { get; init; }
    public string CurrencyName { get; init; }
    public string CurrencySymbol { get; init; }
}
