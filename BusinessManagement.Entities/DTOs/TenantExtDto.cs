namespace BusinessManagement.Entities.DTOs
{
    public class TenantExtDto
    {
        public long TenantId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long AccountId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public string AvatarUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
