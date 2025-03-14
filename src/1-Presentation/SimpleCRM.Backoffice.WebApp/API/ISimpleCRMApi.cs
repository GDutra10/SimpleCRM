using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Backoffice.WebApp.API;

public interface ISimpleCRMApi
{
    Task<(LoginRS?, ValidationRS?, ErrorRS?)> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
    Task<bool> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken);
    Task<(OrderRS?, ValidationRS?, ErrorRS?)> GetOrderAsync(string accessToken, string orderId, CancellationToken cancellationToken);
    Task<(BaseRS?, ValidationRS?, ErrorRS?)> UpdateOrderAsync(string accessToken, string orderId, OrderBackofficeUpdateRQ orderUpdateRQ, CancellationToken cancellationToken);
    Task<(OrderSearchRS?, ValidationRS?, ErrorRS?)> GetOrdersAsync(string accessToken, OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken);

    Task<(InteractionStartRS?, ValidationRS?, ErrorRS?)> StartInteractionAsync(string accessToken, string orderId, CancellationToken cancellationToken);
}