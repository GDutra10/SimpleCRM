using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts.Services;

public interface IInteractionService
{
    Task<InteractionRS> InteractionStartAsync(string token, InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken);

    Task<InteractionRS> InteractionFinishAsync(string token, InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken);

    Task<OrderRS> AddOrderItemAsync(string token, OrderItemAddRQ orderItemAddRQ, CancellationToken cancellationToken);
    Task<OrderRS> DeleteOrderItemAsync(string token, OrderItemDeleteRQ orderItemDeleteRQ, CancellationToken cancellationToken);

    Task<List<InteractionRS>> GetInteractionsInAttendanceAsync(string token, CancellationToken cancellationToken);
}