namespace BusinessManagement.Entities.DTOs;

public record BusinessDto
{
    public int BusinessId { get; init; }
    public long OwnerSystemUserId { get; init; }
    public int BusinessOrder { get; init; }
    public string BusinessName { get; init; }
    public string BusinessCode { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
