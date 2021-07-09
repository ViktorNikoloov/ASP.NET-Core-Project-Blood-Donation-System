namespace BloodDonation.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    public class Recipient : BaseDeletableModel<string>
    {
        public Recipient()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Rating = new HashSet<Rating>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}
