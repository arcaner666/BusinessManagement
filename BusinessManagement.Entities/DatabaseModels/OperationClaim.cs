using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class OperationClaim
    {
        public OperationClaim()
        {
            SystemUserClaims = new HashSet<SystemUserClaim>();
        }

        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }

        public virtual ICollection<SystemUserClaim> SystemUserClaims { get; set; }
    }
}
