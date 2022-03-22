namespace BusinessManagement.Entities.DTOs
{
    public class BusinessDto
    {
        public int BusinessId { get; set; }
        public long OwnerSystemUserId { get; set; }
        public int BusinessOrder { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
