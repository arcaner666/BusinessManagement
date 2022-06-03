namespace BusinessManagement.Entities.DTOs;

public record BankDto
{
    public long BankId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long AccountId { get; init; }
    public long FullAddressId { get; init; }
    public byte CurrencyId { get; init; }
    public string BankName { get; init; }
    public string BankBranchName { get; init; }
    public string BankCode { get; init; }
    public string BankBranchCode { get; init; }
    public string BankAccountCode { get; init; }
    public string Iban { get; init; }
    public string OfficerName { get; init; }
    public short StandartMaturity { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
