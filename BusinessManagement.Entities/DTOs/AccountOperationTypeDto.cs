namespace BusinessManagement.Entities.DTOs;

public record AccountOperationTypeDto
{
    public short AccountOperationTypeId { get; init; }
    public string Title { get; init; }
}
