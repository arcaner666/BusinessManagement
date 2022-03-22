using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Apartment
    {
        public Apartment()
        {
            Flats = new HashSet<Flat>();
        }

        public long ApartmentId { get; set; }
        public int SectionId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long ManagerId { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentCode { get; set; }
        public int BlockNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Flat> Flats { get; set; }
    }
}
