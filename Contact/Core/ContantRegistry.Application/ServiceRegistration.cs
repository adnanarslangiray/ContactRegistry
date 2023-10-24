using ContantRegistry.Application.Mapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Net.NetworkInformation;

namespace ContantRegistry.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
                            cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

        #region ConfigureMapper

        services.AddAutoMapper(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<ContactMappingProfile>();
        }, typeof(ServiceRegistration).Assembly);

        #endregion ConfigureMapper
    }
}