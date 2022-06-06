namespace BusinessManagement.Entities.DTOs;

public class SystemUserPasswordDto
{
    public long SystemUserId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
