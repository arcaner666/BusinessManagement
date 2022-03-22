namespace BusinessManagement.Entities.DTOs
{
    public class FullAddressDto
    {
        public long FullAddressId { get; set; }
        public short CityId { get; set; }
        public int DistrictId { get; set; }
        public string AddressTitle { get; set; }
        public int PostalCode { get; set; }
        public string AddressText { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
