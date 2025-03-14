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

namespace SimpleCRM.Application.Backoffice.Services;

public class InteractionService : BaseService, IInteractionService
{
    private readonly IRepository<Interaction> _interactionRepository;
    private readonly IRepository<Order> _orderRepository;

    public InteractionService(ILogger<BaseService> logger,
        IMapper mapper,
        TokenManager tokenManager,
        UserManager userManager,
        IRepository<Interaction> interactionRepository,
        IRepository<Order> orderRepository) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _interactionRepository = interactionRepository;
        _orderRepository = orderRepository;
    }

    public async Task<InteractionStartRS> StartAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(orderId, cancellationToken);

        if (order is null)
            throw new NotFoundException("Order not found!");

        if (order.OrderState != OrderState.PreConfirmed)
            throw new BusinessException("Can't start the backoffice in Order that is not Pre Confirmed!");
        
        var orderInteraction = await _interactionRepository.GetAsync(order.InteractionId, cancellationToken);
        
        if (orderInteraction is null)
            throw new NotFoundException("Interaction not found!");

        return new InteractionStartRS()
        {
            OrderInteraction = Mapper.Map<Interaction, InteractionRS>(orderInteraction)
            // TODO: add interaction
        };
    }
}