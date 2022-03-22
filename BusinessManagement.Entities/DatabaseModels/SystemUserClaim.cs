using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class SystemUserClaim
    {
        public long SystemUserClaimId { get; set; }
        public long SystemUserId { get; set; }
        public int OperationClaimId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual OperationClaim OperationClaim { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}
