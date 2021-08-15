namespace BloodDonation.Web.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BloodDonation.Common;

    public class EndTimeValidationAttribute : ValidationAttribute
    {
        private readonly string comparisonProperty;

        public EndTimeValidationAttribute(string comparisonProperty)
        {
            this.comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(this.comparisonProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(this.ErrorMessage ?? GlobalConstants.EndTimeValidationAttributeMessage);
            }

            return ValidationResult.Success;
        }
    }
}
