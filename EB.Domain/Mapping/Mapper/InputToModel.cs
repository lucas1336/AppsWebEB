using AutoMapper;
using EB.API.Input;
using EB.Domain.Mapping.Resolver;
using EB.Infrastructure.Models;

namespace EB.API.Mapper;

public class InputToModel : Profile
{
    public InputToModel()
    {
        CreateMap<ProductInput, Product>()
            .ForMember(dest => dest.MonitoringLevel, opt => opt.MapFrom<MonitoringLevelResolverInput>());
        CreateMap<SnapshotInput, Snapshot>();
    }
}