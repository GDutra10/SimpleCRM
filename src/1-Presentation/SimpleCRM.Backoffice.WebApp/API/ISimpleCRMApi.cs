using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Backoffice.WebApp.API;

public interface ISimpleCRMApi
{
    Task<(LoginRS?, ValidationRS?, ErrorRS?)> LoginAsync(LoginRQ loginRQ, CancellationToken cancellationToken);
    Task<bool> ValidateTokenAsync(string accessToken, CancellationToken cancellationToken);
    Task<(OrderRS?, ValidationRS?, ErrorRS?)> GetOrderAsync(string accessToken, string orderId, CancellationToken cancellationToken);
    Task<(BaseRS?, ValidationRS?, ErrorRS?)> UpdateOrderAsync(string accessToken, string orderId, OrderBackofficeUpdateRQ orderUpdateRQ, CancellationToken cancellationToken);
}