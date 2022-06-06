namespace BusinessManagement.Entities.DTOs;

public class AuthorizationDto
{
    public long SystemUserId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Role { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public bool Blocked { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended
    public string Password { get; set; }
    public int RefreshTokenDuration { get; set; }
    public string AccessToken { get; set; }
}
