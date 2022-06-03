namespace BusinessManagement.Entities.DTOs;

public record BranchCodeDto
{
    public int BranchOrder { get; init; }
    public string BranchCode { get; init; }
}
