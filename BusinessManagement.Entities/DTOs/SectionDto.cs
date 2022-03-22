namespace BusinessManagement.Entities.DTOs
{
    public class SectionDto
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
    }
}
