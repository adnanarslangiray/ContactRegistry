using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();
});

//services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

//mapcontrollers
app.MapControllers();
app.UseCors("CorsPolicy");
app.UseOcelot();

app.Run();
