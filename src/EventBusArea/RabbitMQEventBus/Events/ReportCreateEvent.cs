using RabbitMQEventBus.Events.Interfaces;

namespace RabbitMQEventBus.Events;

public class ReportCreateEvent : IEvent
{
    public string ReportId { get; set; }
    public IList<ReportDetailDetailsBus> ReportDetails { get; set; }
}

public class ReportDetailDetailsBus
{
    public int ContactCount { get; set; }
    public int PhoneNumberCount { get; set; }
    public string Location { get; set; }
}