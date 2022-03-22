using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            Businesses = new HashSet<Business>();
            SystemUserClaims = new HashSet<SystemUserClaim>();
        }

        public long SystemUserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public bool Blocked { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<SystemUserClaim> SystemUserClaims { get; set; }
    }
}
