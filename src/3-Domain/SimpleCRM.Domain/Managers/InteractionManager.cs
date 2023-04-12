using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class InteractionManager
{
    private readonly IRepository<Interaction> _interactionRepository;

    public InteractionManager(IRepository<Interaction> interactionRepository) 
    {
        _interactionRepository = interactionRepository;
    }

    public async Task<Interaction> CreateInteraction(User? user, Customer? customer, CancellationToken cancellationToken)
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
}