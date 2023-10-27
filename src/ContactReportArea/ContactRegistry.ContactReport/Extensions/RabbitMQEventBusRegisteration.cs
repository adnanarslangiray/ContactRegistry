using ContactRegistry.ContactReport.EventBusHelpers.Consumers;

namespace ContactRegistry.ContactReport.Extensions;

public static class RabbitMQEventBusRegisteration
{
    public static ReportCreateEventBusConsumer Listener { get; set; }

    public static IApplicationBuilder UseEventBusListener(this IApplicationBuilder app)
    {
        Listener = app.ApplicationServices.GetService<ReportCreateEventBusConsumer>();
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