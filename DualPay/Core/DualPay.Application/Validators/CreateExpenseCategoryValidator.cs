using DualPay.Application.Features.Commands.ExpenseCategories;
using FluentValidation;

namespace DualPay.Application.Validators;

public class CreateExpenseCategoryValidator: AbstractValidator<CreateExpenseCategoryCommandRequest>
{
    public CreateExpenseCategoryValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 20).WithMessage("Category must be between 3 and 20 characters.");

        RuleFor(x => x.Description)
            .Length(5, 100).WithMessage("Category must be between 5 and 20 characters.");
    }
}