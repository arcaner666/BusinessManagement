using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Section
    {
        public Section()
        {
            Apartments = new HashSet<Apartment>();
            Flats = new HashSet<Flat>();
        }

        public int SectionId { get; set; }
        public long SectionGroupId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long ManagerId { get; set; }
        public long FullAddressId { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual FullAddress FullAddress { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual SectionGroup SectionGroup { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Flat> Flats { get; set; }
    }
}
