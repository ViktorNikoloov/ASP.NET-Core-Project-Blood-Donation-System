namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Gender
    {
        [Display(Name = "Липсва")]
        Unknown = 0,

        [Display(Name = "Дете")]
        Kid = 1,

        [Display(Name = "Мъж")]
        Male = 2,

        [Display(Name = "Жена")]
        Female = 3,
    }
}
