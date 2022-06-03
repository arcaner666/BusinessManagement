namespace BusinessManagement.Entities.DTOs;

public record RegisterSectionManagerDto
{
    public string NameSurname { get; init; }
    public string Phone { get; init; }
    public string BusinessName { get; init; }
    public short CityId { get; init; }
    public int DistrictId { get; init; }
    public string AddressText { get; init; }
    public string TaxOffice { get; init; }
    public long TaxNumber { get; init; }
    public long IdentityNumber { get; init; }
}
