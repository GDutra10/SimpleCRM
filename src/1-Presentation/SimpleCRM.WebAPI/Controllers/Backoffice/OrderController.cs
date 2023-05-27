using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Backoffice.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.WebAPI.Controllers.Backoffice;

[Authorize]
[ApiController]
[Route("Backoffice/[controller]s")]
public class OrderController : AppBaseController
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    
    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(OrderSearchRS), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<OrderSearchRS> GetOrders(OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken)
    {
        return await _orderService.SearchOrderAsync(orderSearchRQ, cancellationToken);
    }
    [Authorize]

    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(OrderRS), (int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationRS), (int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<OrderRS> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        return await _orderService.GetOrder(orderId, GetAccessTokenFromHeader(), cancellationToken);
    }

    [HttpPut("{orderId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ValidationRS), (int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task SetState(Guid orderId, OrderBackofficeUpdateRQ request, CancellationToken cancellationToken)
    {
        await _orderService.SetOrderState(orderId, GetAccessTokenFromHeader(), request, cancellationToken);
    }
}