using Application.DTOs;
using FluentValidation;

namespace CatalogAPI.Validations;

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        // Name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Product name can only contain letters, numbers, and spaces.");

        // Description
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .Length(5, 500).WithMessage("Description must be between 5 and 500 characters.");

        // Price
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.")
            .ScalePrecision(2, 18).WithMessage("Price cannot have more than 18 digits in total, with 2 decimal places.");

        // Stock
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be 0 or greater.")
            .LessThanOrEqualTo(10000).WithMessage("Stock cannot exceed 10,000 units.");

        // Category
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .Length(2, 50).WithMessage("Category must be between 2 and 50 characters.");
    }
}
