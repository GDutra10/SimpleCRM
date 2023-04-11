using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class LoginRQValidator : AbstractValidator<LoginRQ>
{
    public LoginRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}