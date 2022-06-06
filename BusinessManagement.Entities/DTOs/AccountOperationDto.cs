namespace BusinessManagement.Entities.DTOs;

public class AccountOperationDto
{
    public long AccountOperationId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public short AccountOperationTypeId { get; set; }
    public long EmployeeId { get; set; }
    public long AccountOperationOrder { get; set; }
    public string Title { get; set; }
    public bool Canceled { get; set; }
    public bool Completed { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
