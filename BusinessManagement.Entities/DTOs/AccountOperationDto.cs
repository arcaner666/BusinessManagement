namespace BusinessManagement.Entities.DTOs;

public record AccountOperationDto
{
    public long AccountOperationId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public short AccountOperationTypeId { get; init; }
    public long EmployeeId { get; init; }
    public long AccountOperationOrder { get; init; }
    public string Title { get; init; }
    public bool Canceled { get; init; }
    public bool Completed { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
