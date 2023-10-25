using RabbitMQEventBus.Events.Interfaces;

namespace RabbitMQEventBus.Events;

public class ReportRequestEvent : IEvent
{
    public string ReportId { get; set; }
}