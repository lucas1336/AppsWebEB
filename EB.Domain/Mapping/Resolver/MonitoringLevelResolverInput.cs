using AutoMapper;
using EB.API.Input;
using EB.Domain.Exceptions;
using EB.Infrastructure.Models;

namespace EB.Domain.Mapping.Resolver;

public class MonitoringLevelResolverInput : IValueResolver<ProductInput, Product, int>
{
    public int Resolve(ProductInput source, Product destination, int destMember, ResolutionContext context)
    {
        if (source.MonitoringLevel == "ESSENTIAL_MONITORING")
        {
            return 0;
        }
        else if (source.MonitoringLevel == "ADVANCED_MONITORING")
        {
            return 1;
        }

        throw new ValidationException("Product monitoring level must be ESSENTIAL_MONITORING or ADVANCED_MONITORING");
    }
}