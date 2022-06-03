namespace BusinessManagement.Entities.DTOs;

public record SystemUserDto
{
    public long SystemUserId { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public byte[] PasswordHash { get; init; }
    public byte[] PasswordSalt { get; init; }
    public string Role { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public bool Blocked { get; init; }
    public string RefreshToken { get; init; }
    public DateTime RefreshTokenExpiryTime { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
