namespace BloodDonation.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;

    public class Recipient : BaseDeletableModel<string>
    {
        public Recipient()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Rating = new HashSet<Rating>();
            this.Appointments = new HashSet<Appointment>();
            this.Massages = new HashSet<Massage>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MiddleNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public Gender Gender { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Massage> Massages { get; set; }
    }
}
