namespace BusinessManagement.Entities.DTOs;

public class RegisterSectionManagerDto
{
    public string NameSurname { get; set; }
    public string Phone { get; set; }
    public string BusinessName { get; set; }
    public short CityId { get; set; }
    public int DistrictId { get; set; }
    public string AddressText { get; set; }
    public string TaxOffice { get; set; }
    public long TaxNumber { get; set; }
    public long IdentityNumber { get; set; }
}
