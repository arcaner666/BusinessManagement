using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class AccountOperationDetail
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

        public virtual Account Account { get; set; }
        public virtual AccountOperation AccountOperation { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
