namespace BloodDonation.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.HospitalConstants;

    public class Hospital : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(HospitalNameMaxLength)]
        public string HospitalName { get; set; }

        [Required]
        [MaxLength(HospitalCityMaxLength)]
        public string HospitalCity { get; set; }

        [Required]
        [MaxLength(HospitalWardNameMaxLength)]
        public string HospitalWardName { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
