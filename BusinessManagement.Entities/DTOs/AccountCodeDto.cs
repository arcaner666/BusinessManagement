namespace BusinessManagement.Entities.DTOs;

public record AccountCodeDto
{
    public int AccountOrder { get; init; }
    public string AccountCode { get; init; }
}
