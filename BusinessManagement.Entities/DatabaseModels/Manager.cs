using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Manager
    {
        public Manager()
        {
            Apartments = new HashSet<Apartment>();
            Sections = new HashSet<Section>();
        }

        public long ManagerId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public string AvatarUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
