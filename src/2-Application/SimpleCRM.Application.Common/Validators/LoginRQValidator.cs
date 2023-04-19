using FluentValidation;

namespace SimpleCRM.Application.Common.Validators;

public class LoginRQValidator : AbstractValidator<LoginRQ>
{
    public LoginRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}