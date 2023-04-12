using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
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
        var interaction = await _interactionManager.CreateInteraction(user, customer, cancellationToken);
        
        await _interactionRepository.SaveAsync(interaction, cancellationToken);

        return Mapper.Map<Interaction, InteractionRS>(interaction);
    }
}