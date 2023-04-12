using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts;

public interface IInteractionService
{
    Task<InteractionRS> InteractionStartAsync(string token, InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken);
}