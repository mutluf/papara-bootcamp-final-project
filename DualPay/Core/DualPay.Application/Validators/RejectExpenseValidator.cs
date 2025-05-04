using DualPay.Application.Features.Commands.Expense;
using FluentValidation;

namespace DualPay.Application.Validators;

public class RejectExpenseValidator: AbstractValidator<RejectExpenseCommandRequest>
{
    public RejectExpenseValidator()
    {
        RuleFor(x => x.RejectionReason)
            .Length(5, 100).WithMessage("Rejection reason must be between 5 and 20 characters.");
    }
}