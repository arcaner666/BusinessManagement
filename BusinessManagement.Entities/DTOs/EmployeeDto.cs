﻿namespace BusinessManagement.Entities.DTOs;

public class EmployeeDto
{
    public long EmployeeId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public long AccountId { get; set; }
    public short EmployeeTypeId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string Notes { get; set; }
    public string AvatarUrl { get; set; }
    public long IdentityNumber { get; set; }
    public bool StillWorking { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? QuitDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
