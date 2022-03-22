using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class AccountOperationType
    {
        public AccountOperationType()
        {
            AccountOperations = new HashSet<AccountOperation>();
        }

        public short AccountOperationTypeId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<AccountOperation> AccountOperations { get; set; }
    }
}
