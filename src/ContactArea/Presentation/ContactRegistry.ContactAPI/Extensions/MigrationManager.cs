using ContactRegistry.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ContactRegistry.ContactAPI.Extensions;

public static class MigrationManager
{
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
            context.Database.Migrate();

            ContactDbSeed.SeedAsync(context).Wait();
        }
        catch (Exception)
        {
            throw;
        }
        return app;
    }
}