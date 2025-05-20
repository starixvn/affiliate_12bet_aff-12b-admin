namespace Fanex.Affiliate.Admin.Core.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class ConditionalRequiredAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public ConditionalRequiredAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propertyName);
            var propertyValue = property?.GetValue(validationContext.ObjectInstance, null);

            var condition = propertyValue as bool?;
            var strValue = value as string;
            var isEmpty = value == null || (strValue != null && string.IsNullOrWhiteSpace(strValue));

            if (condition == true && isEmpty)
            {
                var message = ErrorMessage ?? $"{validationContext.DisplayName} is required.";
                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}
