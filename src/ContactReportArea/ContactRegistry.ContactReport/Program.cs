using ContactRegistry.ContactReport.Data;
using ContactRegistry.ContactReport.Data.Interfaces;
using ContactRegistry.ContactReport.Helpers;
using ContactRegistry.ContactReport.Repositories;
using ContactRegistry.ContactReport.Repositories.Interfaces;
using ContactRegistry.ContactReport.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ContactReportDatabaseSetting>(builder.Configuration.GetSection(nameof(ContactReportDatabaseSetting)));
builder.Services.AddSingleton<IContactReportDatabaseSetting>(sp => sp.GetRequiredService<IOptions<ContactReportDatabaseSetting>>().Value);
builder.Services.AddTransient<IReportContext, ReportContext>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder/*.SetIsOriginAllowed((host) => true)*/
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
//RabbitMQ
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
        if (int.TryParse(builder.Configuration["EventBus:RetryCount"], out var retryCountt))
            retryCount = retryCountt;
    }

    return new DefaultRabbitMQPersistentConnection(factory, retryCount, logger);
}
);
builder.Services.AddSingleton<IRabbitMQEventBusProducer, RabbitMQEventBusProducer>();
//builder.Services.AddSingleton<ReportCreateEventBusConsumer>();
builder.Services.AddSingleton<ReportCreateHelper>();
SwaggerConfigure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

// Configure the HTTP request pipeline.
//eventbus listener rabbitmq
//app.UseEventBusListener();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();

static void SwaggerConfigure(IServiceCollection serviceCollection)
{
    serviceCollection.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Contact Report API Documentation",
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