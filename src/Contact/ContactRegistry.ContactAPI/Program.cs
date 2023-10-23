using ContactRegistry.ContactAPI.Extensions;
using ContactRegistry.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();

builder.Services.AddControllers();

var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();