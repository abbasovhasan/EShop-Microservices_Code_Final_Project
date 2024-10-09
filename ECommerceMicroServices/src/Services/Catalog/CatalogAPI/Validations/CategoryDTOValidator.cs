using Application.DTOs;
using FluentValidation;

namespace CatalogAPI.Validations;

public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
{
    public CategoryDTOValidator()
    {
        // Name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters.")
            .Matches("^[a-zA-Z ]*$").WithMessage("Category name can only contain letters and spaces.");
    }
}
