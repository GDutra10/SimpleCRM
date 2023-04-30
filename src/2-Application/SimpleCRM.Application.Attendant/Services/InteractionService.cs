using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Attendant.Services;

public class InteractionService : BaseService, IInteractionService
{
    private readonly InteractionManager _interactionManager;
    private readonly IRepository<Interaction> _interactionRepository;
    private readonly IRepository<Customer> _customerRepository;

    public InteractionService(
        ILogger<InteractionService> logger, 
        IMapper mapper, 
        TokenManager tokenManager, 
        UserManager userManager,
        IRepository<Customer> customerRepository,
        IRepository<Interaction> interactionRepository,
        InteractionManager interactionManager) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _customerRepository = customerRepository;
        _interactionRepository = interactionRepository;
        _interactionManager = interactionManager;
    }
    
    public async Task<InteractionRS> InteractionStartAsync(string token, InteractionStartRQ interactionStartRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByToken(token, cancellationToken);
        var customer = await _customerRepository.GetAsync(interactionStartRQ.CustomerId, cancellationToken);
        var interaction = await _interactionManager.CreateInteractionAsync(user, customer, cancellationToken);
        
        await _interactionRepository.SaveAsync(interaction, cancellationToken);

        return Mapper.Map<Interaction, InteractionRS>(interaction);
    }

    public async Task<InteractionRS> InteractionFinishAsync(string token, InteractionFinishRQ interactionFinishRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByToken(token, cancellationToken);
        var interaction = await _interactionRepository.GetAsync(interactionFinishRQ.InteractionId, cancellationToken);
        var customer = await _customerRepository.GetAsync(interaction?.CustomerId ?? Guid.Empty, cancellationToken);
        
        await _interactionManager.FinishInteractionAsync(interactionFinishRQ.State, interaction, customer, user, cancellationToken);
        await _customerRepository.SaveAsync(customer!, cancellationToken);
        await _interactionRepository.SaveAsync(interaction!, cancellationToken);

        return Mapper.Map<Interaction, InteractionRS>(interaction!);
    }
}