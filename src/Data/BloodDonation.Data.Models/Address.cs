namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;

    using BloodDonation.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.Recipients = new HashSet<Recipient>();
            this.Donors = new HashSet<Donor>();
        }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }

        public virtual ICollection<Donor> Donors { get; set; }
    }
}
