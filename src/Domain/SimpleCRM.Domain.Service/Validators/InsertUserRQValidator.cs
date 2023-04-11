using FluentValidation;
using SimpleCRM.Domain.Common.DTOs;

namespace SimpleCRM.Domain.Service.Validators;

public class InsertUserRQValidator: AbstractValidator<InsertUserRQ>
{
    public InsertUserRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(6);
        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(3);
    }
}