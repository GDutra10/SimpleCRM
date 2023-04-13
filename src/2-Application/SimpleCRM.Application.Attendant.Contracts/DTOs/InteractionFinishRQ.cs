using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class InteractionFinishRQ : BaseRQ
{
    public Guid InteractionId { get; set; }
    public InteractionState State { get; set; }
}