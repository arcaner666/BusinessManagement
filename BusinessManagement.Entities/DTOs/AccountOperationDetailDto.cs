using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagement.Entities.DTOs;

public class AccountOperationDetailDto
{
    public long AccountOperationDetailId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public long AccountOperationId { get; set; }
    public long AccountId { get; set; }
    public byte CurrencyId { get; set; }
    public string DocumentCode { get; set; }
    public decimal DebitBalance { get; set; }
    public decimal CreditBalance { get; set; }
    public decimal ExchangeRate { get; set; }
}
