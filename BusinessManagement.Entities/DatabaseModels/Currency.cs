using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Currency
    {
        public Currency()
        {
            AccountOperationDetails = new HashSet<AccountOperationDetail>();
            Banks = new HashSet<Bank>();
            Cashes = new HashSet<Cash>();
        }

        public byte CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        public virtual ICollection<AccountOperationDetail> AccountOperationDetails { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Cash> Cashes { get; set; }
    }
}
