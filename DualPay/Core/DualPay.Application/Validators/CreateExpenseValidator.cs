using DualPay.Application.Features.Commands.ExpenseCategories;
using FluentValidation;

namespace DualPay.Application.Validators;

public class CreateExpenseValidator: AbstractValidator<CreateExpenseCommandRequest>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(150).WithMessage("Description must be at most 150 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(150).WithMessage("Location must be at most 150 characters.");

        RuleFor(x => x.DocumentUrl)
            .MaximumLength(100).WithMessage("Document URL must be at most 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.DocumentUrl));
        
        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0).WithMessage("Expense category ID is required and must be greater than 0.");
    }
}