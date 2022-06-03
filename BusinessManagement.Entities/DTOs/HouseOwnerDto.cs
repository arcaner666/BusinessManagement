﻿namespace BusinessManagement.Entities.DTOs;

public record HouseOwnerDto
{
    public long HouseOwnerId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long AccountId { get; init; }
    public string NameSurname { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string Gender { get; init; }
    public string Notes { get; init; }
    public string AvatarUrl { get; init; }
    public string TaxOffice { get; init; }
    public long? TaxNumber { get; init; }
    public long? IdentityNumber { get; init; }
    public short StandartMaturity { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}
