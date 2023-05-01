using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class OrderItemDeleteRQValidator : AbstractValidator<OrderItemDeleteRQ>
{
    public OrderItemDeleteRQValidator()
    {
        RuleFor(i => i.InteractionId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        RuleFor(i => i.OrderItemId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}