using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Application.Common.Services;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Application.Attendant.Services;

public class InteractionService : BaseService, IInteractionService
{
    private readonly InteractionManager _interactionManager;
    private readonly IRepository<Interaction> _interactionRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly OrderManager _orderManager;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;

    public InteractionService(
        ILogger<InteractionService> logger, 
        IMapper mapper, 
        TokenManager tokenManager, 
        UserManager userManager,
        IRepository<Customer> customerRepository,
        IRepository<Interaction> interactionRepository,
        IRepository<Product> productRepository,
        IRepository<OrderItem> orderItemRepository,
        InteractionManager interactionManager,
        OrderManager orderManager) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _customerRepository = customerRepository;
        _interactionRepository = interactionRepository;
        _productRepository = productRepository;
        _interactionManager = interactionManager;
        _orderManager = orderManager;
        _orderItemRepository = orderItemRepository;
    }
    
    public async Task<InteractionRS> InteractionStartAsync(string token, InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(token, cancellationToken);
        var customer = await _customerRepository.GetAsync(interactionStartRQ.CustomerId, cancellationToken);
        var interaction = await _interactionManager.CreateInteractionAsync(user, customer, cancellationToken);
        
        await _interactionRepository.SaveAsync(interaction, cancellationToken);
        await _customerRepository.SaveAsync(customer!, cancellationToken);

        return Mapper.Map<Interaction, InteractionRS>(interaction);
    }

    public async Task<OrderRS> AddOrderItemAsync(string token, OrderItemAddRQ orderItemAddRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(token, cancellationToken);
        var interaction = await _interactionRepository.GetAsync(orderItemAddRQ.InteractionId, cancellationToken);
        var product = await _productRepository.GetAsync(orderItemAddRQ.ProductId, cancellationToken);
        var order = await _orderManager.AddOrderItemAsync(user, interaction, product, cancellationToken);

        return Mapper.Map<Order, OrderRS>(order);
    }

    public async Task<OrderRS> DeleteOrderItemAsync(string token, OrderItemDeleteRQ orderItemDeleteRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(token, cancellationToken);
        var interaction = await _interactionRepository.GetAsync(orderItemDeleteRQ.InteractionId, cancellationToken);
        var orderItem = await _orderItemRepository.GetAsync(orderItemDeleteRQ.OrderItemId, cancellationToken);
        var order = await _orderManager.DeleteOrderItemAsync(user, interaction, orderItem, cancellationToken);

        return Mapper.Map<Order, OrderRS>(order);
    }

    public async Task<InteractionRS> InteractionFinishAsync(string token, InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(token, cancellationToken);
        var interaction = await _interactionRepository.GetAsync(interactionFinishRQ.InteractionId, cancellationToken);
        var customer = await _customerRepository.GetAsync(interaction?.CustomerId ?? Guid.Empty, cancellationToken);

        await _interactionManager.FinishInteractionAsync(interactionFinishRQ.State, interaction, customer, user, cancellationToken);
        UpdateCustomer(customer, interactionFinishRQ.CustomerProps);
        await _customerRepository.SaveAsync(customer!, cancellationToken);
        await _interactionRepository.SaveAsync(interaction!, cancellationToken);

        return Mapper.Map<Interaction, InteractionRS>(interaction!);
    }

    public async Task<List<InteractionRS>> GetInteractionsInAttendanceAsync(string token, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(token, cancellationToken);
        
        if (user is null)
            throw new BusinessException("It is not possible to get an interaction without user!");

        var interactionsInAttendance = await _interactionRepository
            .GetAllAsync(new InteractionUserInAttendanceSpecification(user), cancellationToken);

        return Mapper.Map<List<Interaction>, List<InteractionRS>>(interactionsInAttendance);
    }
    
    private static void UpdateCustomer(Customer? customer, CustomerProps? customerProps)
    {
        if (customer == null || customerProps == null) 
            return;
        
        if (customerProps.Name is not null)
            customer.Name = customerProps.Name;
            
        if (customerProps.Email is not null)
            customer.Email = customerProps.Email;
            
        if (customerProps.Telephone is not null)
            customer.Telephone = customerProps.Telephone;
    }
}