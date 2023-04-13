using System.Collections.ObjectModel;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class InteractionManager
{
    private readonly IRepository<Interaction> _interactionRepository;
    public static ReadOnlyCollection<InteractionState> FinishStatus = new ReadOnlyCollection<InteractionState>(new List<InteractionState>() { InteractionState.NotInterested }); 
    
    public InteractionManager(IRepository<Interaction> interactionRepository) 
    {
        _interactionRepository = interactionRepository;
    }

    public async Task<Interaction> CreateInteractionAsync(User? user, Customer? customer, CancellationToken cancellationToken)
    {
        if (user is null)
            throw new BusinessException("Can't start interaction without user!");
        
        if (customer is null)
            throw new BusinessException("Can't start interaction without customer!");
        
        var interactionsInAttendance = await _interactionRepository
            .GetAllAsync(new InteractionInAttendanceSpecification(customer.Id), cancellationToken);

        if (interactionsInAttendance.Any())
            throw new BusinessException($"Can't interact with customer '{customer.Name}'. He is already in attendance!");
        
        return await Task.FromResult(new Interaction(user, customer));
    }

    public async Task FinishInteractionAsync(InteractionState state, Interaction? interaction, User? userRequested, CancellationToken cancellationToken)
    {
        if (userRequested is null)
            throw new BusinessException("Can't finish interaction without user!");

        if (interaction is null)
            throw new BusinessException("Can't finish interaction! Interaction doesn't exist!");
        
        if (interaction.UserId != userRequested.Id)
            throw new BusinessException($"The user '{userRequested.Name}' can't finish the interaction: {interaction.Id}!");

        if (!FinishStatus.Contains(state))
            throw new BusinessException($"Can't finalize the interaction '{interaction.Id}' with state '{state}'!"); 
        
        interaction.EndTime = DateTime.Now;
        interaction.InteractionState = state;

        await Task.CompletedTask;
    }
}