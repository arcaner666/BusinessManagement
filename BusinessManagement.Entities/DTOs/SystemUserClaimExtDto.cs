namespace BusinessManagement.Entities.ExtendedDatabaseModels;

public class SystemUserClaimExtDto
{
    public long SystemUserClaimId { get; set; }
    public long SystemUserId { get; set; }
    public int OperationClaimId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With OperationClaim
    public string OperationClaimName { get; set; }
}
