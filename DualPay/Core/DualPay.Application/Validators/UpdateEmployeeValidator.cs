using DualPay.Application.Features.Commands;
using FluentValidation;

namespace DualPay.Application.Validators;
public class UpdateEmployeeValidator: AbstractValidator<UpdateEmployeeCommandRequest>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$").WithMessage("Phone number must be exactly 10 digits.")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.AccountNumber)
            .MaximumLength(30).WithMessage("Account number must be at most 30 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.AccountNumber));
    }
}