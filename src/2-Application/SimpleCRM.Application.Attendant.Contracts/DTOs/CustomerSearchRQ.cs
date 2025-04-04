﻿namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class CustomerSearchRQ : BaseSearchRQ
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Telephone { get; set; } = default!;
}