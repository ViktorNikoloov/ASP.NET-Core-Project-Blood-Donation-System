namespace BloodDonation.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;

    public class Donor : BaseDeletableModel<string>
    {
        public Donor()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Appointments = new HashSet<Appointment>();
            this.Massages = new HashSet<Massage>();
        }

        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(MiddleNameMaxLength)]
        public string MiddleName { get; set; }

        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public Gender Gender { get; set; }

        public BloodType BloodType { get; set; }

        public int DonationCount { get; set; }

        public DateTime LastDonation { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Massage> Massages { get; set; }
    }
}
