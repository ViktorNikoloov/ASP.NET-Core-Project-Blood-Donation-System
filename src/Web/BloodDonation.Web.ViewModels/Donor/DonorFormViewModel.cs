namespace BloodDonation.Web.ViewModels.Donor
{
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models;
    using BloodDonation.Data.Models.Enums;

    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;

    public class DonorFormViewModel
    {
        [Required]
        [MinLength(FirstNameMinLength, ErrorMessage ="Прекалено късо първо име.")]
        [MaxLength(FirstNameMaxLength, ErrorMessage = "Прекалено дълго първо име.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(MiddleNameMinLength, ErrorMessage = "Прекалено късо средно име.")]
        [MaxLength(MiddleNameMaxLength, ErrorMessage = "Прекалено дълго средно име.")]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(LastNameMinLength, ErrorMessage = "Прекалено късо фамилно име.")]
        [MaxLength(LastNameMaxLength, ErrorMessage = "Прекалено дълго фамилно име.")]
        public string LastName { get; set; }

        [Required]
        [Url(ErrorMessage ="Невалитен \"Url\" адрес")]
        public string ImageUrl { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; }

        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; }
    }
}
