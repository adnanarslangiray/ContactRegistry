using ContactRegistry.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistry.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ContactDbContext>(options => options.UseNpgsql(Configurations.GetConnectionString));


    }


     
}
