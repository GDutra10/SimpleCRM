﻿namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class InteractionRS : BaseRS
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime EndTime { get; set; }
    public UserRS User { get; set; } = default!;
    public CustomerRS Customer { get; set; } = default!;
}