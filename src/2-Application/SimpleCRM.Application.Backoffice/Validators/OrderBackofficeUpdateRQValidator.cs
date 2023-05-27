using FluentValidation;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Backoffice.Validators;

public class OrderBackofficeUpdateRQValidator : AbstractValidator<OrderBackofficeUpdateRQ>
{
    public OrderBackofficeUpdateRQValidator()
    {
        RuleFor(x => x.OrderState)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Must(orderState => orderState is OrderState.Confirmed or OrderState.Returned or OrderState.Canceled);
    }   
}