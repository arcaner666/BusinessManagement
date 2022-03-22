﻿using System;
using System.Collections.Generic;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class Customer
    {
        public long CustomerId { get; set; }
        public int BusinessId { get; set; }
        public long BranchId { get; set; }
        public long AccountId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public string AvatarUrl { get; set; }
        public int AppointmentsMade { get; set; }
        public int ProductsPurchased { get; set; }
        public DateTimeOffset? LastPurchaseDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public virtual Account Account { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Business Business { get; set; }
    }
}
