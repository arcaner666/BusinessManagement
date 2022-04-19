namespace BusinessManagement.Entities.DTOs;

public class AccountExtDto
{
    public long AccountId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public short AccountGroupId { get; set; }
    public short AccountTypeId { get; set; }
    public int AccountOrder { get; set; }
    public string AccountName { get; set; }
    public string AccountCode { get; set; }
    public string TaxOffice { get; set; }
    public long? TaxNumber { get; set; }
    public long? IdentityNumber { get; set; }
    public decimal DebitBalance { get; set; }
    public decimal CreditBalance { get; set; }
    public decimal Balance { get; set; }
    public decimal Limit { get; set; }
    public short StandartMaturity { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With Branch
    public string BranchName { get; set; }

    // Extended With AccountGroup
    public string AccountGroupName { get; set; }
    public string AccountGroupCode { get; set; }

    // Extended With AccountType
    public string AccountTypeName { get; set; }

    // Added Custom Required Fields
    public string NameSurname { get; set; }
    public string Phone { get; set; }
}
