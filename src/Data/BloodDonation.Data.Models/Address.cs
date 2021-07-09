using System.Collections.Generic;

namespace BloodDonation.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int PostCode { get; set; }

        public virtual ICollection<Donor> Donors { get; set; }

    }
}
