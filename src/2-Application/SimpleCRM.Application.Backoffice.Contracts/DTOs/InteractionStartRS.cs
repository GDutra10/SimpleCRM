using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Backoffice.Contracts.DTOs;

public class InteractionStartRS : BaseRS
{
    public InteractionRS OrderInteraction { get; set; } = default!;
    public InteractionRS Interaction { get; set; } = default!;
}