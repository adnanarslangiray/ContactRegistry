using ContantRegistry.Application.Mapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ContantRegistry.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
                            cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);

        #region ConfigureMapper

        //services.AddAutoMapper(typeof(ContactMappingProfile));
        services.AddAutoMapper(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<ContactMappingProfile>();
        }, typeof(ServiceRegistration).Assembly);

        #endregion ConfigureMapper

        // json configuration
        //JsonConvert Global Setting
        JsonConvert.DefaultSettings = () =>
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            settings.Converters.Add(new StringEnumConverter());
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
        };
    }
}