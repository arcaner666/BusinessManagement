namespace BusinessManagement.Entities.DTOs;

public class ManagerExtDto
{
    public long ManagerId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Notes { get; set; }
    public string AvatarUrl { get; set; }
    public string TaxOffice { get; set; }
    public long? TaxNumber { get; set; }
    public long? IdentityNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With Business
    public string BusinessName { get; set; }

    // Extended With Branch + FullAddress
    public short CityId { get; set; }
    public int DistrictId { get; set; }
    public string AddressText { get; set; }
}
