namespace BusinessManagement.Entities.DTOs;

public record AccountDto
{
    public long AccountId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public short AccountGroupId { get; init; }
    public short AccountTypeId { get; init; }
    public int AccountOrder { get; init; }
    public string AccountName { get; init; }
    public string AccountCode { get; init; }
    public decimal DebitBalance { get; init; }
    public decimal CreditBalance { get; init; }
    public decimal Balance { get; init; }
    public decimal Limit { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
