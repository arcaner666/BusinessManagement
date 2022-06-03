namespace BusinessManagement.Entities.DTOs;

public record SectionGroupDto
{
    public long SectionGroupId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public string SectionGroupName { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
