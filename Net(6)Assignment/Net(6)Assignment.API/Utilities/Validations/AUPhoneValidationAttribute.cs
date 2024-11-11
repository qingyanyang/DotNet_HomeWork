using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Net_6_Assignment.Utilities.Validations
{
    public class AUPhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                string stringValue = value.ToString();
                var regex = new Regex(@"^\+61\s4\d{8}$");
                if (!regex.IsMatch(stringValue))
                {
                    return new ValidationResult("This is not a valid AU phone number!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
