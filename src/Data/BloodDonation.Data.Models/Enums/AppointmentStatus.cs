namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum AppointmentStatus
    {
        [Display(Name = "Обикновенна")]
        Basic = 1,

        [Display(Name = "Спешна")]
        Emergency = 2,

    }
}
