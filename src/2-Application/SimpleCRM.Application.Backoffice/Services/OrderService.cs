using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Backoffice.Contracts.DTOs;
using SimpleCRM.Application.Backoffice.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Application.Common.Services;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Application.Backoffice.Services;

public class OrderService :  BaseService, IOrderService
{
    private readonly OrderManager _orderManager;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;

    public OrderService(
        ILogger<BaseService> logger, 
        IMapper mapper, 
        TokenManager tokenManager, 
        UserManager userManager,
        OrderManager orderManager,
        IRepository<Order> orderRepository,
        IRepository<User> userRepository) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _orderManager = orderManager;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
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

    public async Task<OrderRS> GetOrder(Guid orderId, string accessToken, CancellationToken cancellationToken)
    {
        var id = TokenManager.GetId(accessToken);
        var userRequest = await _userRepository.GetAsync(id, cancellationToken);

        if (userRequest is null)
            throw new BusinessException("Invalid User!");
        
        if (userRequest.Role != Role.BackOffice)
            throw new BusinessException($"User {userRequest.Name} dont have permission!");

        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        if (order is null)
            throw new NotFoundException("Order not found!");
        
        return Mapper.Map<Order, OrderRS>(order);
    }

    public async Task SetOrderState(Guid orderId, string accessToken, OrderBackofficeUpdateRQ request, CancellationToken cancellationToken)
    {
        var id = TokenManager.GetId(accessToken);
        var userRequest = await _userRepository.GetAsync(id, cancellationToken);

        if (userRequest is null)
            throw new BusinessException("Invalid User!");
        
        if (userRequest.Role != Role.BackOffice)
            throw new BusinessException($"User {userRequest.Name} dont have permission!");
        
        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        if (order is null)
            throw new NotFoundException("Order not found!");

        order.OrderState = request.OrderState;
        await _orderRepository.SaveAsync(order, cancellationToken);
    }
}