using AutoMapper;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Common.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderRS>();
    }
}