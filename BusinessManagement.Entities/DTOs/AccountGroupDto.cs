namespace BusinessManagement.Entities.DTOs;

public record AccountGroupDto
{
    public short AccountGroupId { get; init; }
    public string AccountGroupName { get; init; }
    public string AccountGroupCode { get; init; }
}
