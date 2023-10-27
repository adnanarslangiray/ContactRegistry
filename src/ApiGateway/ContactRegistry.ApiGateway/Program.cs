using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder/*.SetIsOriginAllowed((host) => true)*/
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
Task<IApplicationBuilder> task = app.UseOcelot();

app.Run();
