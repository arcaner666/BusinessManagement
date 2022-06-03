namespace BusinessManagement.Entities.DTOs;

public record DistrictDto
{
    public int DistrictId { get; init; }
    public short CityId { get; init; }
    public string DistrictName { get; init; }
}
