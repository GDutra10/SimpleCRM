using AutoMapper;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Common.Profiles;

public class InteractionProfile : Profile
{
    public InteractionProfile()
    {
        CreateMap<Interaction, InteractionRS>();
    }
}