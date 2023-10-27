using ContactRegistry.ContactAPI.EventBusHelpers.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace ContactRegistry.ContactAPI.Extensions;

public static class RabbitMQEventBusRegisteration
{
    public static ReportPreparationEventBusConsumer Listener { get; set; }

    public static IApplicationBuilder UseEventBusListener(this IApplicationBuilder app)
    {
        Listener = app.ApplicationServices.GetService<ReportPreparationEventBusConsumer>();
        var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        life.ApplicationStarted.Register(OnStarted);
        life.ApplicationStopping.Register(OnStopping);
        return app;
    }

    private static void OnStarted()
    {
        Listener.Consume();
    }
    private static void OnStopping()
    {
           Listener.Disconnect();
    }
}
