namespace BusinessManagement.Entities.DTOs;

public record AccountTypeDto
{
    public short AccountTypeId { get; init; }
    public string AccountTypeName { get; init; }
}
