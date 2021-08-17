namespace BloodDonation.Services.Data.DTO
{
    using BloodDonation.Web.Infrastructure;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GetAppointmentById
    {
        public int Id { get; set; }

        public string HospitalName { get; set; }

        public string HospitalWard { get; set; }

        public string HospitalCity { get; set; }

        public int BloodBankCount { get; set; }

        public string SendingAddressInfo { get; set; }

        [Display(Name = "Необходимо от")]
        [DataType(DataType.Date)]
        [DateValidation]
        public DateTime StartDate { get; set; }

        [Display(Name = "Краен срок")]
        [DataType(DataType.Date)]
        [EndTimeValidation("StartDate")]
        public DateTime DeadLine { get; set; }

        public bool IsApproved { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
