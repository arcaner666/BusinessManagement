﻿namespace BusinessManagement.Entities.DTOs;

public class CashExtDto
{
    public long CashId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public long AccountId { get; set; }
    public byte CurrencyId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With Account
    public short AccountGroupId { get; set; }
    public int AccountOrder { get; set; }
    public string AccountName { get; set; }
    public string AccountCode { get; set; }
    public string TaxOffice { get; set; }
    public long? TaxNumber { get; set; }
    public long? IdentityNumber { get; set; }
    public decimal Limit { get; set; }
    public short StandartMaturity { get; set; }
}