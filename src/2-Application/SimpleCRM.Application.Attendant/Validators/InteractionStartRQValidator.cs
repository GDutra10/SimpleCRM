using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class InteractionStartRQValidator : AbstractValidator<InteractionStartRQ>
{
    public InteractionStartRQValidator()
    {
        RuleFor(x => x.CustomerId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}