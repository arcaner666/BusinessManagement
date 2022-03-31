namespace BusinessManagement.Entities.DTOs;

public class BranchExtDto
{
    public long BranchId { get; set; }
    public int BusinessId { get; set; }
    public long FullAddressId { get; set; }
    public int BranchOrder { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With FullAddress
    public short CityId { get; set; }
    public int DistrictId { get; set; }
    public string AddressTitle { get; set; }
    public int PostalCode { get; set; }
    public string AddressText { get; set; }
}
