using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Bank
    {
        public long BankId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long AccountId { get; set; }
        public long FullAddressId { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankCode { get; set; }
        public string BankBranchCode { get; set; }
        public string BankAccountCode { get; set; }
        public string Iban { get; set; }
        public string OfficerName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Account Account { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual FullAddress FullAddress { get; set; }
    }
}
