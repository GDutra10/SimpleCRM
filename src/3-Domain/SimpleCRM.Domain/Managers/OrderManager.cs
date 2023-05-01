using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class OrderManager
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Interaction> _interactionRepository;

    public OrderManager(
        IRepository<Order> orderRepository, 
        IRepository<OrderItem> orderItemRepository,
        IRepository<Interaction> interactionRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _interactionRepository = interactionRepository;
    }

    private void ValidateCommonOrderItem(User? userRequested, Interaction? interaction, bool isAdding)
    {
        var addOrRemove = isAdding ? "add" : "remove"; 
        
        if (userRequested is null)
            throw new BusinessException($"It is not possible to {addOrRemove} an Order ITem without user!");
        
        if (interaction is null)
            throw new BusinessException($"It is not possible to {addOrRemove} an Order Item without interaction!");

        if (userRequested.Id != interaction.UserId)
            throw new BusinessException($"It is not possible to {addOrRemove} an Order Item, User is not the same of the Interaction!");

        if (interaction.InteractionState != InteractionState.InAttendance)
            throw new BusinessException($"It is not possible to {addOrRemove} an Order Item, Interaction is not in Attendance!");
    }
    
    public async Task<Order> AddOrderItemAsync(User? userRequested, Interaction? interaction, Product? product, CancellationToken cancellationToken)
    {
        ValidateCommonOrderItem(userRequested, interaction, true);
        
        if (product is null)
            throw new BusinessException("It is not possible to add an Order Item with invalid Product!");
        
        var order = await GetOrCreateOrderAsync(interaction, cancellationToken);
        var orderItem = new OrderItem(order.Id, product);
        order.OrderItems.Add(orderItem);

        await _orderItemRepository.SaveAsync(orderItem, cancellationToken);
        await _orderRepository.SaveAsync(order, cancellationToken);

        return order;
    }

    public async Task<Order> DeleteOrderItemAsync(User? userRequested, Interaction? interaction, OrderItem? orderItem, CancellationToken cancellationToken)
    {
        ValidateCommonOrderItem(userRequested, interaction, false);
        
        if (orderItem is null)
            throw new BusinessException("It is not possible to remove an Order Item with invalid Order Item!");

        var order = (await _orderRepository.GetAsync(orderItem.OrderId, cancellationToken))!;
        order.OrderItems.Remove(orderItem);
        
        await _orderItemRepository.DeleteAsync(orderItem, cancellationToken);
        await _orderRepository.SaveAsync(order, cancellationToken);
        
        return order;
    }
    
    private async Task<Order> GetOrCreateOrderAsync(Interaction? interaction, CancellationToken cancellationToken)
    {
        if (interaction is null)
            throw new BusinessException("It is not possible to create an Order without interaction");
        
        var orders = await _orderRepository.GetAllAsync(new GetOrderSpecification(interaction.Id), cancellationToken);
        var order = orders.FirstOrDefault();

        if (order is not null) 
            return order;
        
        order = new Order(interaction);
        await _orderRepository.SaveAsync(order, cancellationToken);
        interaction.Order = order;
        interaction.OrderId = order.Id;
        await _interactionRepository.SaveAsync(interaction, cancellationToken);
        
        return order;
    }
}