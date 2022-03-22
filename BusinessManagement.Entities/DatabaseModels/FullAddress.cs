using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class FullAddress
    {
        public FullAddress()
        {
            Banks = new HashSet<Bank>();
            Branches = new HashSet<Branch>();
            Sections = new HashSet<Section>();
        }

        public long FullAddressId { get; set; }
        public short CityId { get; set; }
        public int DistrictId { get; set; }
        public string AddressTitle { get; set; }
        public int PostalCode { get; set; }
        public string AddressText { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual City City { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Bank> Banks { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
