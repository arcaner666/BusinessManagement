namespace BusinessManagement.Entities.DTOs
{
    public class SectionExtDto
    {
        public int SectionId { get; set; }
        public long SectionGroupId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long ManagerId { get; set; }
        public long FullAddressId { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        // Extended With SectionGroup
        public string SectionGroupName { get; set; }

        // Extended With Manager
        public string ManagerNameSurname { get; set; }

        // Extended With FullAddress
        public short CityId { get; set; }
        public int DistrictId { get; set; }
        public string AddressTitle { get; set; }
        public int PostalCode { get; set; }
        public string AddressText { get; set; }

        // Extended With FullAddress + City
        public string CityName { get; set; }

        // Extended With FullAddress + District
        public string DistrictName { get; set; }
    }
}
