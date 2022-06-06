namespace BusinessManagement.Entities.DTOs;

public class SystemUserClaimDto
{
    public long SystemUserClaimId { get; set; }
    public long SystemUserId { get; set; }
    public int OperationClaimId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
