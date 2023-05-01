using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class OrderManager
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Order> _orderRepository;

    public OrderManager(
        IRepository<Order> orderRepository, 
        IRepository<OrderItem> orderItemRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<Order> AddOrderItemAsync(Interaction? interaction, Product product, CancellationToken cancellationToken)
    {
        var order = await GetOrCreateOrderAsync(interaction, cancellationToken);
        var orderItem = new OrderItem(order.Id, product);
        order.OrderItems.Add(orderItem);

        await _orderItemRepository.SaveAsync(orderItem, cancellationToken);
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

        return order;
    }
}