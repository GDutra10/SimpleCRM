using AutoMapper;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Attendant.Profiles;

public class InteractionProfile : Profile
{
    public InteractionProfile()
    {
        CreateMap<Interaction, InteractionRS>();
    }
}