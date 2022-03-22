using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Flat
    {
        public long FlatId { get; set; }
        public int SectionId { get; set; }
        public long ApartmentId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long? HouseOwnerId { get; set; }
        public long? TenantId { get; set; }
        public string FlatCode { get; set; }
        public int DoorNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual HouseOwner HouseOwner { get; set; }
        public virtual Section Section { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
