namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
            this.Hospitals = new HashSet<Hospital>();
        }

        [MaxLength(CityMaxLength)]
        public string Name { get; set; }

        [MaxLength(PostCodeMaxValue)]
        public int PostCode { get; set; }

        public int StreetId { get; set; }

        public virtual Street Street { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
