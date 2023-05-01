using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class OrderItemAddRQValidator : AbstractValidator<OrderItemAddRQ>
{
    public OrderItemAddRQValidator()
    {
        RuleFor(i => i.InteractionId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
        RuleFor(i => i.ProductId).Cascade(CascadeMode.Stop).NotNull().NotEmpty();
    }
}