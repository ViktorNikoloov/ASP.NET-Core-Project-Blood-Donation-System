namespace BloodDonation.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum BloodType
    {
        [Display(Name = "A+")]
        APositive = 1,

        [Display(Name = "A-")]
        ANegative = 2,

        [Display(Name = "B+")]
        BPositive = 1,

        [Display(Name = "B-")]
        BNegative = 2,

        [Display(Name = "AB+")]
        ABPositive = 1,

        [Display(Name = "AB-")]
        ABNegative = 2,

        [Display(Name = "0+")]
        ZeroPositive = 1,

        [Display(Name = "0-")]
        ZeroNegative = 2,

    }
}
