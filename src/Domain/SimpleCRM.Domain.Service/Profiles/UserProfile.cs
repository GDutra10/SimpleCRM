using AutoMapper;
using SimpleCRM.Domain.Common.DTOs;
using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Service.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, InsertUserRQ>();
    }
}