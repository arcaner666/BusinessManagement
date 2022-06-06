namespace BusinessManagement.Entities.DTOs;

public class SectionGroupDto
{
    public long SectionGroupId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public string SectionGroupName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
