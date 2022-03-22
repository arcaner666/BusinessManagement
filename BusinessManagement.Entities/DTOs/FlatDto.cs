namespace BusinessManagement.Entities.DTOs
{
    public class FlatDto
    {
        public long FlatId { get; set; }
        public int SectionId { get; set; }
        public long ApartmentId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long? HouseOwnerId { get; set; }
        public long? TenantId { get; set; }
        public string FlatCode { get; set; }
        public int DoorNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
