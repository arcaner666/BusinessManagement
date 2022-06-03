namespace BusinessManagement.Entities.DTOs;

public record FlatDto
{
    public long FlatId { get; init; }
    public int SectionId { get; init; }
    public long ApartmentId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long? HouseOwnerId { get; init; }
    public long? TenantId { get; init; }
    public string FlatCode { get; init; }
    public int DoorNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
