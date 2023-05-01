using AutoMapper;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Application.Common.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductRS>();
    }
}