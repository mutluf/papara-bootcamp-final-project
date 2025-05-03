using DualPay.Application.Features.Commands.ExpenseCategories;
using FluentValidation;

namespace DualPay.Application.Validators;

public class UpdateExpenseCategoryValidator: AbstractValidator<UpdateExpenseCategoryCommandRequest>
{
    public UpdateExpenseCategoryValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 20).WithMessage("Category must be between 3 and 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Description)
            .Length(5, 100).WithMessage("Category must be between 5 and 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));;
    }
}