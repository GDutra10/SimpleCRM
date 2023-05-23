using SimpleCRM.Application.Backoffice.Contracts.DTOs;

namespace SimpleCRM.Application.Backoffice.Contracts.Services;

public interface IOrderService
{
    Task<OrderSearchRS> SearchOrderAsync(OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken);
}