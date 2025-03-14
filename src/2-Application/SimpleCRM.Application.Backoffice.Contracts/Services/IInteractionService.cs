using SimpleCRM.Application.Backoffice.Contracts.DTOs;

namespace SimpleCRM.Application.Backoffice.Contracts.Services;

public interface IInteractionService
{
    Task<InteractionStartRS> StartAsync(Guid orderId, CancellationToken cancellationToken);
}