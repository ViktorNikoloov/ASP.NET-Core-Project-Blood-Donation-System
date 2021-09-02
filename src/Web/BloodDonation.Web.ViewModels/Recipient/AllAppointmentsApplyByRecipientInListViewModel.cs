namespace BloodDonation.Web.ViewModels.Recipient
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models.Enums;

    public class AllAppointmentsApplyByRecipientInListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Кръвна група")]
        public BloodType BloodType { get; set; }

        [Display(Name = "Начална дата")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Краен срок")]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Информация за изпращане")]
        public string SendingAddressInfo { get; set; }

        [Display(Name = "Допълнителна информация ")]
        public string AdditionalInfo { get; set; }

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
