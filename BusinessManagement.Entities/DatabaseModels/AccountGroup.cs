using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class AccountGroup
    {
        public AccountGroup()
        {
            Accounts = new HashSet<Account>();
        }

        public short AccountGroupId { get; set; }
        public string AccountGroupName { get; set; }
        public string AccountGroupCode { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
