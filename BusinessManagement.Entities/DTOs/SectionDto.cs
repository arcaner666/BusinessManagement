namespace BusinessManagement.Entities.DTOs;

public record SectionDto
{
    public int SectionId { get; init; }
    public long SectionGroupId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long ManagerId { get; init; }
    public long FullAddressId { get; init; }
    public string SectionName { get; init; }
    public string SectionCode { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
