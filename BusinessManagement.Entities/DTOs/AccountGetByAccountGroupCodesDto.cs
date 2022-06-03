namespace BusinessManagement.Entities.DTOs;

public record AccountGetByAccountGroupCodesDto
{
    public int BusinessId { get; init; }

    public string[] AccountGroupCodes { get; init; }
}
