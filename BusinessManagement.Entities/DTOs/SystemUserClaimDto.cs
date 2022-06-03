namespace BusinessManagement.Entities.DTOs;

public record SystemUserClaimDto
{
    public long SystemUserClaimId { get; init; }
    public long SystemUserId { get; init; }
    public int OperationClaimId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
