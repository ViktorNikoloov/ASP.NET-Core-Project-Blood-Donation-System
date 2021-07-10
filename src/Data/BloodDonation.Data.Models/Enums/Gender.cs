namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum Gender
    {
        [Display(Name = "Мъж.")]
        Male = 1,

        [Display(Name = "Жена.")]
        Female = 2,

        [Display(Name = "Дете.")]
        Kid = 3,
    }
}
