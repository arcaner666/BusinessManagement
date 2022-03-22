using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class SectionGroup
    {
        public SectionGroup()
        {
            Sections = new HashSet<Section>();
        }

        public long SectionGroupId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public string SectionGroupName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
