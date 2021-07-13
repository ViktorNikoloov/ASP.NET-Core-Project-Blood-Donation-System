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
            this.Countries = new HashSet<Country>();
        }

        [Required]
        [MaxLength(CityMaxLength)]
        public string Name { get; set; }

        [MaxLength(PostCodeMaxLength)]
        public int PostCode { get; set; }

        [MaxLength(StreetMaxLength)]
        public string Street { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
