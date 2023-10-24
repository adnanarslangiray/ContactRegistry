﻿using ContactRegistry.Persistence.Contexts;
using ContactRegistry.Persistence.Repositories.Contact;
using ContactRegistry.Persistence.Services;
using ContantRegistry.Application.Abstractions.Services;
using ContantRegistry.Application.Repositories.Contact;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistry.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ContactDbContext>(options
                => options.UseNpgsql(Configurations.GetConnectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

        // repositories
        services.AddScoped<IContactReadRepository, ContactReadRepository>();
        services.AddScoped<IContactWriteRepository, ContactWriteRepository>();

        //services
        services.AddScoped<IContactService, ContactService>();
    }
}