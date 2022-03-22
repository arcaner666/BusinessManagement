using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class District
    {
        public District()
        {
            FullAddresses = new HashSet<FullAddress>();
        }

        public int DistrictId { get; set; }
        public short CityId { get; set; }
        public string DistrictName { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<FullAddress> FullAddresses { get; set; }
    }
}
