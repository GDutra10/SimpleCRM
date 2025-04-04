﻿using FluentValidation;
using SimpleCRM.Application.Admin.Contracts.DTOs;

namespace SimpleCRM.Application.Admin.Validators;

public class ProductUpdateRQValidator : AbstractValidator<ProductUpdateRQ>
{
    public ProductUpdateRQValidator()
    {
        RuleFor(p => p.Name).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(3);
    }
}