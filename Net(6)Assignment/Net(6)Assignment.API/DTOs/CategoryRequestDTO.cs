using Net_6_Assignment.Common.Enums;
using Net_6_Assignment.Utilities.Validations;
using System.ComponentModel.DataAnnotations;

namespace Net_6_Assignment.DTOs
{
    public class CategoryRequestDTO
    {

        [Required(ErrorMessage="Category name is required!")]
        [MaxLength(50, ErrorMessage = "Category name can be up to 50 characters only!")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category level is required!")]
        public CategoryLevel CategoryLevel { get; set; }

        // if CategoryLevel is parent level, parent id is required
        // otherwise it is null
        [ParentIdValidationAttribute()]
        public Guid? ParentId { get; set; }
    }

    public class CategoryUpdateRequestDTO : CategoryRequestDTO
    {
        [Required(ErrorMessage = "Category id is required!")]
        public Guid Id { get; set; }
    }
}
