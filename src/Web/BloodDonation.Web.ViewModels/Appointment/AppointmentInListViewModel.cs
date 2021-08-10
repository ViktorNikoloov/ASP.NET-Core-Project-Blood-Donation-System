namespace BloodDonation.Web.ViewModels.Appointment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Services.Mapping;

    public class AppointmentInListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string RecipientFirstName { get; set; }

        [Display(Name = "Кръв")]
        [EnumDataType(typeof(BloodType))]
        public BloodType BloodType { get; set; }

        [Display(Name = "Банки кръв")]
        public int BloodBankCount { get; set; }

        [Display(Name = "Краен срок")]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string AdditionalInfo { get; set; }

        public string ImageUrl { get; set; }
    }
}
