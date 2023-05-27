using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Application.Backoffice.Contracts.Services;

public interface IOrderService
{
    Task<OrderSearchRS> SearchOrderAsync(OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken);
    Task<OrderRS> GetOrder(Guid orderId, string accessToken, CancellationToken cancellationToken);
    Task SetOrderState(Guid orderId, string accessToken, OrderBackofficeUpdateRQ request, CancellationToken cancellationToken);
}