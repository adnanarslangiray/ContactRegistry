using ContactRegistry.ContactAPI.Extensions;
using ContactRegistry.Persistence;
using ContantRegistry.Application;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

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

SwaggerConfigure(builder.Services);


builder.Services.AddControllers();

var app = builder.Build();
app.MigrateDatabase();

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