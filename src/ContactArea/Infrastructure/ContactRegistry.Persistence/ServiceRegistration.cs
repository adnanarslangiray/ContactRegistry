using ContactRegistry.Persistence.Contexts;
using ContactRegistry.Persistence.Repositories.Contact;
using ContactRegistry.Persistence.Repositories.ContactFeatures;
using ContactRegistry.Persistence.Services;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.Repositories.Contact;
using ContantRegistry.Application.Repositories.ContactFeature;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistry.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ContactDbContext>(options
                => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")), ServiceLifetime.Transient, ServiceLifetime.Transient);

        // repositories
        services.AddSingleton<IContactReadRepository, ContactReadRepository>();
        services.AddSingleton<IContactWriteRepository, ContactWriteRepository>();
        services.AddSingleton<IContactFeatureWriteRepository, ContactFeaturesWriteRepository>();
        services.AddSingleton<IContactFeatureReadRepository, ContactFeaturesReadRepository>();

        //services
        services.AddSingleton<IContactService, ContactService>();
    }
}