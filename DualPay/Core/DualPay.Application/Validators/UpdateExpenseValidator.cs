using DualPay.Application.Features.Commands.ExpenseCategories;
using FluentValidation;

namespace DualPay.Application.Validators;

public class UpdateExpenseValidator: AbstractValidator<UpdateExpenseCommandRequest>
{
    public UpdateExpenseValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.")
            .When(x => x.Amount != null);

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("Description must be at most 150 characters.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Location)
            .MaximumLength(150).WithMessage("Location must be at most 150 characters.")
            .When(x => !string.IsNullOrEmpty(x.Location));

        RuleFor(x => x.DocumentUrl)
            .MaximumLength(100).WithMessage("Document URL must be at most 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.DocumentUrl));
        
        RuleFor(x => x.ExpenseCategoryId)
            .GreaterThan(0).WithMessage("Expense category ID must be greater than 0.")
            .When(x => x.ExpenseCategoryId.HasValue);
    }
}