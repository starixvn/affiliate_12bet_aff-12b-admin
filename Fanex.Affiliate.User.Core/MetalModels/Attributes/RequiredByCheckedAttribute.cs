using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.MetalModels.Attributes
{
    public class RequiredByCheckedAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} field is required";

        public string DependentPropertyName { get; set; }

        public bool IsChecked { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isChecked = false;
            //Get details about the dependent property
            var dependentProperty = validationContext.ObjectInstance.GetType().GetProperty(DependentPropertyName);
            var dependentPropertyValue = Convert.ToString(dependentProperty.GetValue(validationContext.ObjectInstance, null));

            if (bool.TryParse(dependentPropertyValue, out isChecked))
            {
                //Validation should fail if the checkbox is selected but the main field is empty
                if (isChecked == IsChecked && string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    return new ValidationResult(string.IsNullOrEmpty(FormatErrorMessage(validationContext.DisplayName)) ?
                        string.Format(DefaultErrorMessage, validationContext.DisplayName) :
                        FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ValidationType = "requiredbychecked";
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("dependentprop", DependentPropertyName);
            rule.ValidationParameters.Add("checked", IsChecked);
            yield return rule;
        }
    }
}
