namespace BloodDonation.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Common.Models;

    using static BloodDonation.Common.DataGlobalConstants.AppointmentConstants;

    public class Appointment : BaseDeletableModel<int>
    {
        public Appointment()
        {
            this.Appointments = new HashSet<AppointmetsDonors>();
        }

        public virtual Hospital Hospital { get; set; }

        [Required]
        [MaxLength(BloodBankCountMaxLength)]
        public int BloodBankCount { get; set; }

        [Required]
        public string SendingAddressInfo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DeadLine { get; set; }

        public bool IsApproved { get; set; }

        public string AdditionalInfo { get; set; }

        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }

        public virtual ICollection<AppointmetsDonors> Appointments { get; set; }

    }
}
