using DualPay.Application.Features.Commands;
using FluentValidation;

namespace DualPay.Application.Validators;
public class CreateEmployeeValidator: AbstractValidator<CreateEmployeeCommandRequest>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be at most 50 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname must be at most 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not in a correct format.")
            .MaximumLength(100).WithMessage("Email must be at most 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{10}$").WithMessage("Phone number must be exactly 10 digits.");

        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("Account number is required.")
            .MaximumLength(30).WithMessage("Account number must be at most 30 characters.");

        RuleFor(x => x.IdentityNumber)
            .NotEmpty().WithMessage("Identity number is required.")
            .Length(11).WithMessage("Identity number must be exactly 11 characters.")
            .Matches(@"^\d{11}$").WithMessage("Identity number must contain only digits.");
    }
}