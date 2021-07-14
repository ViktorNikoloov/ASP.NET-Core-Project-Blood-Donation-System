namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        [Range(StreetNumberMinValue, StreetNumberMaxValue)]
        public int Number { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

    }
}
