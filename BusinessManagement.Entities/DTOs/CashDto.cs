namespace BusinessManagement.Entities.DTOs;

public record CashDto
{
    public long CashId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long AccountId { get; init; }
    public byte CurrencyId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
