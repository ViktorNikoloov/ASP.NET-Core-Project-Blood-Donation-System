namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Addresses = new HashSet<Address>();
        }

        [Required]
        [MaxLength(CountryMaxLength)]
        public string Name { get; set; }

        public string Code { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }
}
