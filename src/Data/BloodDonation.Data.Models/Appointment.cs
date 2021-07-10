namespace BloodDonation.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    using static BloodDonation.Common.DataGlobalConstants.AppointmentConstants;

    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;

    public class Appointment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MiddleNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [MaxLength(HospitalNameMaxLength)]
        public string HospitalName { get; set; }

        [Required]
        [MaxLength(HospitalCityMaxLength)]
        public string HospitalCity { get; set; }

        [Required]
        [MaxLength(HospitalWardNameMaxLength)]
        public string HospitalWardName { get; set; }

        [Required]
        [MaxLength(BloodBankCountMaxLength)]
        public int BloodBankCount { get; set; }

        public BloodType BloodType { get; set; }

        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; }

        [Required]
        public string SendingAddressInfo { get; set; }

        public DateTime PublishedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public AppointmentStatus Status { get; set; }

        public string SentBy { get; set; }

        public string Comment { get; set; }

        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }
    }
}
