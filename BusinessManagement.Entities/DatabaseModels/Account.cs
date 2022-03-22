using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Account
    {
        public Account()
        {
            AccountOperationDetails = new HashSet<AccountOperationDetail>();
            Banks = new HashSet<Bank>();
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            HouseOwners = new HashSet<HouseOwner>();
            Tenants = new HashSet<Tenant>();
        }

        public long AccountId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public short AccountGroupId { get; set; }
        public byte CurrencyId { get; set; }
        public int AccountOrder { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public string TaxOffice { get; set; }
        public long? TaxNumber { get; set; }
        public long? IdentityNumber { get; set; }
        public decimal DebitBalance { get; set; }
        public decimal CreditBalance { get; set; }
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }
        public short StandartMaturity { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual AccountGroup AccountGroup { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<AccountOperationDetail> AccountOperationDetails { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<HouseOwner> HouseOwners { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
