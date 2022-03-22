using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class AccountOperation
    {
        public AccountOperation()
        {
            AccountOperationDetails = new HashSet<AccountOperationDetail>();
        }

        public long AccountOperationId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public short AccountOperationTypeId { get; set; }
        public long EmployeeId { get; set; }
        public long AccountOperationOrder { get; set; }
        public string Title { get; set; }
        public bool Canceled { get; set; }
        public bool Completed { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual AccountOperationType AccountOperationType { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<AccountOperationDetail> AccountOperationDetails { get; set; }
    }
}
