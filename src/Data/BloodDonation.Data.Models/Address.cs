namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;

    public class Address : BaseDeletableModel<int>
    {
        public Address()
        {
            this.Recipients = new HashSet<Recipient>();
            this.Donors = new HashSet<Donor>();
        }

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        [MaxLength(StreetMaxLength)]
        public string Street { get; set; }

        [MaxLength(PostCodeMaxLength)]
        public int PostCode { get; set; }

        public virtual ICollection<Recipient> Recipients { get; set; }

        public virtual ICollection<Donor> Donors { get; set; }
    }
}
