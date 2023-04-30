using System.Collections.ObjectModel;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class InteractionManager
{
    public static readonly ReadOnlyCollection<InteractionState?> AvailableStatus = new ReadOnlyCollection<InteractionState?>(new List<InteractionState?>() { null, InteractionState.NotAvailable, InteractionState.InAttendance });
    public static readonly ReadOnlyCollection<InteractionState?> FinishStatus = new ReadOnlyCollection<InteractionState?>(new List<InteractionState?>() { InteractionState.NotInterested, InteractionState.NotAvailable });
    public static readonly ReadOnlyCollection<InteractionState?> FinalizedStatus = new ReadOnlyCollection<InteractionState?>(new List<InteractionState?>() { InteractionState.NotInterested, InteractionState.Sale, InteractionState.PreSale });
    private readonly IRepository<Interaction> _interactionRepository;

    public InteractionManager(IRepository<Interaction> interactionRepository) 
    {
        _interactionRepository = interactionRepository;
    }

    public async Task<Interaction> CreateInteractionAsync(User? user, Customer? customer, CancellationToken cancellationToken)
    {
        if (user is null)
            throw new BusinessException("It is not possible to start an interaction without user!");
        
        if (customer is null)
            throw new BusinessException("It is not possible to start an interaction without customer!");
        
        var interactionsInAttendance = await _interactionRepository
            .GetAllAsync(new InteractionInAttendanceSpecification(customer.Id), cancellationToken);

        if (interactionsInAttendance.Any())
            throw new BusinessException($"It is not possible to interact with customer '{customer.Name}'. He is already in attendance!");

        if (FinalizedStatus.Contains(customer.State))
            throw new BusinessException($"It is not possible to interact with customer '{customer.Name}'! Customer is finalized!");
        
        return await Task.FromResult(new Interaction(user, customer));
    }

    public async Task FinishInteractionAsync(InteractionState state, Interaction? interaction, Customer? customer, User? userRequested, CancellationToken cancellationToken)
    {
        if (userRequested is null)
            throw new BusinessException("It is not possible to end an interaction without user!");

        if (interaction is null)
            throw new BusinessException("It is not possible to end an interaction! The interaction doesn't exist!");
        
        if (interaction.UserId != userRequested.Id)
            throw new BusinessException($"The user '{userRequested.Name}' can't finish the interaction: {interaction.Id}!");

        if (interaction.InteractionState != InteractionState.InAttendance)
            throw new BusinessException($"It is not possible to end an interaction that has already ended!");
        
        if (!FinishStatus.Contains(state))
            throw new BusinessException($"It is not possible to end an interaction '{interaction.Id}' with state '{state}'!");

        if (customer is null)
            throw new BusinessException($"The customer '{interaction.CustomerId}' doesn't exist");
        
        customer.State = state;
        interaction.EndTime = DateTime.Now;
        interaction.InteractionState = state;
        interaction.Customer.State = state;

        await Task.CompletedTask;
    }
}