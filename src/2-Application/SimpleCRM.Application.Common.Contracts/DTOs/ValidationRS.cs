﻿using System.Text.Json.Serialization;

namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class ValidationRS : BaseRS
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Validation>? Validations { get; set; }

    public void AddValidation(string field, string message)
    {
        Validations ??= new List<Validation>();
        Validations.Add(new Validation() { Field = field, Message = message });
    }
}