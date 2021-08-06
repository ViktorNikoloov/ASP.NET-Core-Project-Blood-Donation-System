namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum BloodType
    {
        [Display(Name = "Липсва")]
        Unknown = 0,

        [Display(Name = "A(+)")]
        APositive = 1,

        [Display(Name = "A(-)")]
        ANegative = 2,

        [Display(Name = "B(+)")]
        BPositive = 3,

        [Display(Name = "B(-)")]
        BNegative = 4,

        [Display(Name = "AB(+)")]
        ABPositive = 5,

        [Display(Name = "AB(-)")]
        ABNegative = 6,

        [Display(Name = "0(+)")]
        ZeroPositive = 7,

        [Display(Name = "0(-)")]
        ZeroNegative = 8,
    }
}
