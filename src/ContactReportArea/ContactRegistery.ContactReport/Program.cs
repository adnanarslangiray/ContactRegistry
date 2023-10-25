using ContactRegistery.ContactReport.Data;
using ContactRegistery.ContactReport.Data.Interfaces;
using ContactRegistery.ContactReport.Repositories;
using ContactRegistery.ContactReport.Repositories.Interfaces;
using ContactRegistery.ContactReport.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ContactReportDatabaseSetting>(builder.Configuration.GetSection(nameof(ContactReportDatabaseSetting)));
builder.Services.AddSingleton<IContactReportDatabaseSetting>(sp => sp.GetRequiredService<IOptions<ContactReportDatabaseSetting>>().Value);
builder.Services.AddTransient<IReportContext, ReportContext>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();



SwaggerConfigure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

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
