namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.StreetConstants;

    public class Street : BaseDeletableModel<int>
    {
        public Street()
        {
            this.Towns = new HashSet<Town>();
        }

        [MaxLength(StreetNameMaxLength)]
        public string Name { get; set; }

        [NotMapped] // TODO: Make a related table with numbers.
        [Range(StreetNumberMinValue, StreetNumberMaxValue)]
        public int Number { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
