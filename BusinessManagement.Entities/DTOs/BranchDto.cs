namespace BusinessManagement.Entities.DTOs;

public record BranchDto
{
    public long BranchId { get; init; }
    public int BusinessId { get; init; }
    public long FullAddressId { get; init; }
    public int BranchOrder { get; init; }
    public string BranchName { get; init; }
    public string BranchCode { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
