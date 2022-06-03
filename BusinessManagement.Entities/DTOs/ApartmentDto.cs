namespace BusinessManagement.Entities.DTOs;

public record ApartmentDto
{
    public long ApartmentId { get; init; }
    public int SectionId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long ManagerId { get; init; }
    public string ApartmentName { get; init; }
    public string ApartmentCode { get; init; }
    public int BlockNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
