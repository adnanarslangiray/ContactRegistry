using ContactRegistry.ContactAPI.Consumers;
using ContactRegistry.ContactAPI.Extensions;
using ContactRegistry.Persistence;
using ContantRegistry.Application;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

// versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

//RabbitMQ

#region RabbitMQ MessageBroker

builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };

    if (!string.IsNullOrEmpty(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
    }

    if (!string.IsNullOrEmpty(builder.Configuration["EventBus:Password"]))
    {
        factory.Password = builder.Configuration["EventBus:Password"];
    }

    var retryCount = 5;
    if (!string.IsNullOrEmpty(builder.Configuration["EventBus:RetryCount"]))
    {
        retryCount = int.Parse(builder.Configuration["EventBus:RetryCount"]);
    }

    return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
}
);
builder.Services.AddSingleton<RabbitMQEventBusProducer>();
builder.Services.AddSingleton<ReportPreparationEventBusConsumer>();

#endregion RabbitMQ MessageBroker

SwaggerConfigure(builder.Services);

builder.Services.AddControllers();

var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
//eventbus listener rabbitmq
app.UseEventBusListener();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SwaggerConfigure(IServiceCollection serviceCollection)
{
    serviceCollection.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Contact API Documentation",
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Email = "apihelp@adnanarslangiray.com",
                Name = "Adnan Arslangiray",
                Url = new Uri("https://adnanarslangiray.com")
            },
        });
    });
}