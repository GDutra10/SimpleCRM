using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class CustomerRegisterRQValidator : AbstractValidator<CustomerRegisterRQ>
{
    public CustomerRegisterRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(3);
    }
}