namespace BloodDonation.Web.ViewModels.Donor
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models.Enums;

    using static BloodDonation.Common.DataGlobalConstants.AddressConstants;
    using static BloodDonation.Common.DataGlobalConstants.BasicUserInfoConstants;
    using static BloodDonation.Common.DataGlobalConstants.StreetConstants;

    public class DonorFormViewModel
    {
        public const string RequiredFirstNameError = "Полето \"Имe\" е задължително.";

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MinLength(FirstNameMinLength)]
        [MaxLength(FirstNameMaxLength)]
        [StringLength(FirstNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [Display(Name = "Бащино име")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MinLength(MiddleNameMinLength)]
        [MaxLength(MiddleNameMaxLength)]
        [StringLength(MiddleNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = MiddleNameMinLength)]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MinLength(LastNameMinLength)]
        [MaxLength(LastNameMaxLength)]
        [StringLength(LastNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        [Display(Name = "Url адрес")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [Url(ErrorMessage ="Невалиден \"Url\" адрес")]
        public string ImageUrl { get; set; }

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")] // The attribute is put on the Enum type, only to set random error message.
        [EnumDataType(typeof(Gender))]
        [Range(1, 2, ErrorMessage = "Полето \"{0}\" трябва да съдържа \"{1}\" или \"{2}\".")]
        public Gender Gender { get; set; }

        [Display(Name = "Град")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [MinLength(CityMinLength)]
        [MaxLength(CityMaxLength)]
        [StringLength(CityMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = CityMinLength)]
        public string CityName { get; set; }

        [Display(Name = "Пощенски код")]
        [Range(PostCodeMinValue, PostCodeMaxValue, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{1}\" и \"{2}\" символа.")]
        public int? PostCode { get; set; }

        [Display(Name = "Улица")]
        [MinLength(StreetNameMinLength)]
        [MaxLength(StreetNameMaxLength)]
        [StringLength(StreetNameMaxLength, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = StreetNameMinLength)]
        public string StreetName { get; set; }

        [Display(Name = "Телефонен номер")]
        [RegularExpression(PhoneNumberRegex, ErrorMessage ="Тел. номер трябва да започва с \"+359\" или \"0\" и да съдържа общо \"10\" цифри.")]
        public string PhoneNumber { get; set; }
    }
}
