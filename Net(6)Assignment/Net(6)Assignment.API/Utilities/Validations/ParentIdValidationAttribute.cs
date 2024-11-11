using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Net_6_Assignment.Utilities.Validations
{
    public class ParentIdValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dto = (CategoryRequestDTO)validationContext.ObjectInstance;

            // Check if CategoryLevel requires ParentId
            if (dto.CategoryLevel == CategoryLevel.ChildLevel && dto.ParentId == null)
            {
                return new ValidationResult("Parent ID is required for child-level categories.");
            }

            return ValidationResult.Success;
        }
    }
}
