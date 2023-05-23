using FluentValidation;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Backoffice.Validators;

public class OrderSearchRQValidator : AbstractValidator<OrderSearchRQ>
{
    public OrderSearchRQValidator()
    {
        RuleFor(x => x.OrderState)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Must(state => state == OrderState.PreConfirmed);
    }
}