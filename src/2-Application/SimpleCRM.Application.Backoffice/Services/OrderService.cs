using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Backoffice.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Application.Common.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Application.Backoffice.Services;

public class OrderService :  BaseService, IOrderService
{
    private readonly OrderManager _orderManager;
    private readonly IRepository<Order> _orderRepository;

    public OrderService(
        ILogger<BaseService> logger, 
        IMapper mapper, 
        TokenManager tokenManager, 
        UserManager userManager,
        OrderManager orderManager,
        IRepository<Order> orderRepository) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _orderManager = orderManager;
        _orderRepository = orderRepository;
    }
    
    public async Task<OrderSearchRS> SearchOrderAsync(OrderSearchRQ orderSearchRQ, CancellationToken cancellationToken)
    {
        var orders = await _orderManager.GetOrdersAsync(orderSearchRQ.OrderState, orderSearchRQ.PageNumber, orderSearchRQ.PageSize, cancellationToken);
        
        var totalRecord = await _orderRepository.CountAsync(new OrderSearchSpecification(orderSearchRQ.OrderState), cancellationToken);
        var hasData = totalRecord > 0;
        var list = Mapper.Map<List<Order>, List<OrderRS>>(orders);
        var totalPages = hasData ? Math.Ceiling((double)totalRecord / (double)orderSearchRQ.PageSize) : 0;
        
        if (totalPages == 0)
            totalPages++;

        return new OrderSearchRS
        {
            Records = list,
            PageSize = orderSearchRQ.PageSize,
            CurrentPage = orderSearchRQ.PageNumber,
            TotalPages = (int)totalPages,
            TotalRecords = totalRecord
        };
    }
}