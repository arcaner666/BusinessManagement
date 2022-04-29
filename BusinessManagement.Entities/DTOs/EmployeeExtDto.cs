namespace BusinessManagement.Entities.DTOs;

public class EmployeeExtDto
{
    public long EmployeeId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public long AccountId { get; set; }
    public short EmployeeTypeId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Notes { get; set; }
    public string AvatarUrl { get; set; }
    public bool StillWorking { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? QuitDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    
    // Extended With Account
    public short AccountGroupId { get; set; }
    public int AccountOrder { get; set; }
    public string AccountName { get; set; }
    public string AccountCode { get; set; }
    public string TaxOffice { get; set; }
    public long? TaxNumber { get; set; }
    public long? IdentityNumber { get; set; }
    public decimal Limit { get; set; }
    public short StandartMaturity { get; set; }

    // Extended With EmployeeType
    public string EmployeeTypeName { get; set; }
}
