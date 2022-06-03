using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagement.Entities.DTOs;

public record AccountOperationDetailDto
{
    public long AccountOperationDetailId { get; init; }
    public int BusinessId { get; init; }
    public long BranchId { get; init; }
    public long AccountOperationId { get; init; }
    public long AccountId { get; init; }
    public byte CurrencyId { get; init; }
    public string DocumentCode { get; init; }
    public decimal DebitBalance { get; init; }
    public decimal CreditBalance { get; init; }
    public decimal ExchangeRate { get; init; }
}
