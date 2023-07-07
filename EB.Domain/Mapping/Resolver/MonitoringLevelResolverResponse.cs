using AutoMapper;
using EB.API.Input;
using EB.Domain.Exceptions;
using EB.Infrastructure.Models;

namespace EB.Domain.Mapping.Resolver;

public class MonitoringLevelResolverResponse : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
        if (source.MonitoringLevel == 0)
        {
            return "ESSENTIAL_MONITORING";
        }
        else if (source.MonitoringLevel == 1)
        {
            return "ADVANCED_MONITORING";
        }

        return default!;
    }
}