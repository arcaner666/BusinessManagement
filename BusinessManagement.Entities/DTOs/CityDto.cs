namespace BusinessManagement.Entities.DTOs;

public record CityDto
{
    public short CityId { get; init; }
    public int PlateCode { get; init; }
    public string CityName { get; init; }
}
