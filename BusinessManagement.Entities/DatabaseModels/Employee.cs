using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Employee
    {
        public Employee()
        {
            AccountOperations = new HashSet<AccountOperation>();
        }

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
        public bool StillWorking { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Account Account { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual ICollection<AccountOperation> AccountOperations { get; set; }
    }
}
