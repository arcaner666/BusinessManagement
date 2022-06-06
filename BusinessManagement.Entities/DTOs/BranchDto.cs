namespace BusinessManagement.Entities.DTOs;

public class BranchDto
{
    public long BranchId { get; set; }
    public int BusinessId { get; set; }
    public long FullAddressId { get; set; }
    public int BranchOrder { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
