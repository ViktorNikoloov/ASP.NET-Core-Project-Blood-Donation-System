namespace BloodDonation.Services.Data.DTO
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Data.Models.Enums;
    using BloodDonation.Web.Infrastructure;

    using static BloodDonation.Common.DataGlobalConstants.AppointmentConstants;

    public class GetAppointmentById
    {
        public int Id { get; set; }

        [Display(Name = "Имена на ресипиента")]
        public string RecipientFullName { get; set; }

        [Display(Name = "Име на Болница")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(30, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 5)]
        public string HospitalName { get; set; }

        [Display(Name = "Болнично отделение")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(30, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 5)]
        public string HospitalWard { get; set; }

        [Display(Name = "Града на Болницата")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [RegularExpression(@"[А-я]+", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(30, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 5)]
        public string HospitalCity { get; set; }

        [Display(Name = "Банки кръв")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [Range(1, BloodBankCountMaxLength, ErrorMessage = "Можете да изберете от {1} до {2} {0} кръв.")]
        public int BloodBankCount { get; set; }

        public BloodType BloodType { get; set; }

        [Display(Name = "Начален срок")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [DataType(DataType.Date)]
        //[DateValidation]
        public DateTime StartDate { get; set; }

        [Display(Name = "Краен срок")]
        [Required(ErrorMessage = "Полето \"{0}\" е задължително.")]
        [DataType(DataType.Date)]
        [EndTimeValidation("StartDate")]
        public DateTime DeadLine { get; set; }

        [Display(Name = "Допълнителна информация")]
        [RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(300, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 0)]
        public string AdditionalInfo { get; set; }

        [Display(Name = "Начин и адрес за получаване")]
        [RegularExpression(@"[А-я]+.*\s*[А-я]*", ErrorMessage = "Полето \"{0}\" трябва да е на кирилица")]
        [StringLength(150, ErrorMessage = "Полето \"{0}\" трябва да съдържа между \"{2}\" и \"{1}\" символа.", MinimumLength = 0)]
        public string SendingAddressInfo { get; set; }

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
