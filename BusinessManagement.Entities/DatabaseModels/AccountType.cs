using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public short AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
