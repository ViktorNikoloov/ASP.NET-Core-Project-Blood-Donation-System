namespace BloodDonation.Web.ViewModels.Appointment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models.Enums;


    public class AppointmentInListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string RecipientFirstName { get; set; }

        [Display(Name = "Кръвна група")]
        public BloodType BloodType { get; set; }

        [Display(Name = "Банки кръв")]
        public int BloodBankCount { get; set; }

        [Display(Name = "Краен срок")]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Допълнителна информация")]
        public string AdditionalInfo { get; set; }

        public string ImageUrl { get; set; }

        public string EnumDisplayName
            => this.EnumHelperDisplayName(this.BloodType);

        private string EnumHelperDisplayName(BloodType bloodType)
        {
            string enumDisplayName = string.Empty;

            if (bloodType == BloodType.Unknown)
            {
                enumDisplayName = "Липсва";
            }
            else if (bloodType == BloodType.APositive)
            {
                enumDisplayName = "A(+)";
            }
            else if (bloodType == BloodType.ANegative)
            {
                enumDisplayName = "A(-)";
            }
            else if (bloodType == BloodType.BPositive)
            {
                enumDisplayName = "B(+)";
            }
            else if (bloodType == BloodType.BNegative)
            {
                enumDisplayName = "B(-)";
            }
            else if (bloodType == BloodType.ABPositive)
            {
                enumDisplayName = "AB(+)";
            }
            else if (bloodType == BloodType.ABNegative)
            {
                enumDisplayName = "AB(-)";
            }
            else if (bloodType == BloodType.ZeroPositive)
            {
                enumDisplayName = "0(+)";
            }
            else if (bloodType == BloodType.ZeroNegative)
            {
                enumDisplayName = "0(-)";
            }

            return enumDisplayName;
        }
    }

    
}
