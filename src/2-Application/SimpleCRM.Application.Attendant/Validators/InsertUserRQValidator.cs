﻿using FluentValidation;
using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Validators;

public class InsertUserRQValidator: AbstractValidator<UserRegisterRQ>
{
    public InsertUserRQValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(6);
        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(3);
        RuleFor(x => x.Role).Cascade(CascadeMode.Stop).IsInEnum();
    }
}