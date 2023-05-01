using AutoMapper;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Common.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, OrderItemRS>();
    }
}