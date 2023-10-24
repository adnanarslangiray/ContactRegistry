using ContactRegistry.Persistence.Contexts;
using ContactRegistry.Persistence.Repositories;
using ContactRegistry.Persistence.Services;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistry.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ContactDbContext>(options
                => options.UseNpgsql(Configurations.GetConnectionString),
                ServiceLifetime.Singleton,
                ServiceLifetime.Singleton);

        // repositories
        services.AddScoped<IContactReadRepository, ContactReadRepository>();
        services.AddScoped<IContactWriteRepository, ContactWriteRepository>();

        //services
        services.AddScoped<IContactService, ContactService>();
    }
}