using AutoMapper;
using EB.API.Input;
using EB.Domain.Mapping.Resolver;
using EB.Infrastructure.Models;

namespace EB.Domain.Mapping.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.MonitoringLevel, opt => opt.MapFrom<MonitoringLevelResolverResponse>());
    }
}