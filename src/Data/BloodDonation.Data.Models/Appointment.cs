namespace BloodDonation.Data.Models
{
    using System;

    using BloodDonation.Data.Common.Models;
    using BloodDonation.Data.Models.Enums;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string HospitalName { get; set; }

        public string HospitalCity { get; set; }

        public string HospitalWardName { get; set; }

        public int BloodBank { get; set; }

        public BloodType BloodType { get; set; }

        public string PhoneNumber { get; set; }

        public string SendingInfo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime DeadLine { get; set; }

        public AppointmentStatus Status { get; set; }

        public string Comment { get; set; }

        public string DonorId { get; set; }

        public virtual Donor Donor { get; set; }

        public string RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }
    }
}
