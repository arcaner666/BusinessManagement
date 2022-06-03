namespace BusinessManagement.Entities.DTOs;

public record FullAddressDto
{
    public long FullAddressId { get; init; }
    public short CityId { get; init; }
    public int DistrictId { get; init; }
    public string AddressTitle { get; init; }
    public int PostalCode { get; init; }
    public string AddressText { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
