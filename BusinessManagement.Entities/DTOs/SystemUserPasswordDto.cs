namespace BusinessManagement.Entities.DTOs;

public record SystemUserPasswordDto
{
    public long SystemUserId { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}
