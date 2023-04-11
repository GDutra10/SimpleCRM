using FluentValidation;
using SimpleCRM.Domain.Common.DTOs;

namespace SimpleCRM.Domain.Service.Validators;

public class LoginRQValidator : AbstractValidator<LoginRQ>
{
    public LoginRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}