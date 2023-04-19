using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts.Services;

public interface IInteractionService
{
    Task<InteractionRS> InteractionStartAsync(string token, InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken);

    Task<InteractionRS> InteractionFinishAsync(string token, InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken);
}