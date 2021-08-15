namespace BloodDonation.Web.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Common;

    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;

            if (DateTime.UtcNow.AddDays(-1).CompareTo(value) <= 0 && DateTime.UtcNow.AddMonths(1).CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GlobalConstants.DateValidationAttributeMessage);
            }
        }
    }
}
