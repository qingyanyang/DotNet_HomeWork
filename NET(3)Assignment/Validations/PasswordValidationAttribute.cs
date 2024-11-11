using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NET3Assignment.Validations
{
    public class PasswordValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) 
            {
                return new ValidationResult("Password is required!");
            }
            
            string stringValue = value.ToString();
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,15}$");
            if (!regex.IsMatch(stringValue))
            {
                return new ValidationResult("Password must be 8-15 characters long, " +
                    "contain at least one uppercase letter, and at least one special character.");
            }
            
            return ValidationResult.Success;
        }
    }
}
