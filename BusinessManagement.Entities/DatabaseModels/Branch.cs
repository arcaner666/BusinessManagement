using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Branch
    {
        public Branch()
        {
            AccountOperationDetails = new HashSet<AccountOperationDetail>();
            AccountOperations = new HashSet<AccountOperation>();
            Accounts = new HashSet<Account>();
            Apartments = new HashSet<Apartment>();
            Banks = new HashSet<Bank>();
            Cashes = new HashSet<Cash>();
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Flats = new HashSet<Flat>();
            HouseOwners = new HashSet<HouseOwner>();
            Managers = new HashSet<Manager>();
            SectionGroups = new HashSet<SectionGroup>();
            Sections = new HashSet<Section>();
            Tenants = new HashSet<Tenant>();
        }

        public long BranchId { get; set; }
        public int BusinessId { get; set; }
        public long FullAddressId { get; set; }
        public int BranchOrder { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Business Business { get; set; }
        public virtual FullAddress FullAddress { get; set; }
        public virtual ICollection<AccountOperationDetail> AccountOperationDetails { get; set; }
        public virtual ICollection<AccountOperation> AccountOperations { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Cash> Cashes { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Flat> Flats { get; set; }
        public virtual ICollection<HouseOwner> HouseOwners { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<SectionGroup> SectionGroups { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
