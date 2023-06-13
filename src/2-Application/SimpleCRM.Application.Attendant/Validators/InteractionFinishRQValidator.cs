using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class InteractionFinishRQValidator : AbstractValidator<InteractionFinishRQ>
{
    public InteractionFinishRQValidator()
    {
        RuleFor(x => x.CustomerProps)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .SetInheritanceValidator(v => v.Add<CustomerProps>(new CustomerPropsValidator()));
        RuleFor(x => x.InteractionId).Cascade(CascadeMode.Stop).NotNull();
        RuleFor(x => x.CustomerProps!.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
    }
}