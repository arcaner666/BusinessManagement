using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
            FullAddresses = new HashSet<FullAddress>();
        }

        public short CityId { get; set; }
        public int PlateCode { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<FullAddress> FullAddresses { get; set; }
    }
}
