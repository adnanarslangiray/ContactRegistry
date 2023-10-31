using ContactRegistry.ContactReport.Entities;
using ContactRegistry.ContactReport.Repositories.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus;
using RabbitMQEventBus.Constants;
using RabbitMQEventBus.Events;
using System.Text;

namespace ContactRegistry.ContactReport.Helpers.EventBusHelpers.Consumers;

public class ReportCreateEventBusConsumer
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly IReportRepository _reportRepository;

    public ReportCreateEventBusConsumer(IRabbitMQPersistentConnection persistentConnection, IReportRepository reportRepository)
    {
        _persistentConnection = persistentConnection;
        _reportRepository = reportRepository;
    }

    public void Consume()
    {
        if (_persistentConnection.IsConnected == false)
            _persistentConnection.TryConnect();

        var channel = _persistentConnection.CreateModel();
        channel.QueueDeclare(queue: EventConstants.ContactReportCreateQueue, durable: true, false, false, null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;
        channel.BasicConsume(EventConstants.ContactReportCreateQueue, true, consumer);
    }

    private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var @reportCreateEvent = JsonConvert.DeserializeObject<ReportCreateEvent>(message);
        if (e.RoutingKey == EventConstants.ContactReportCreateQueue)
        {
            await ContactReportProcess(@reportCreateEvent);
        }
    }
    public void Disconnect()
    {
        _persistentConnection.Dispose();
    }
    private async Task ContactReportProcess(ReportCreateEvent createEvent)
    {
        var report = await _reportRepository.GetReportByIdAsync(createEvent.ReportId);
        if (report is null)
            return;
        if (createEvent.ReportDetails.Count == 0)
        {
            await _reportRepository.UpdateReportStatusAsync(createEvent.ReportId, Report.ReportStatus.Completed);//failed verilebilir!
            return;
        }

        IList<ReportDetail> detailData = createEvent.ReportDetails
            .Select(x => new ReportDetail() { ReportId = createEvent.ReportId, ContactCount = x.ContactCount, Location = x.Location, PhoneNumberCount = x.PhoneNumberCount })
            .ToList();

        await _reportRepository.CreateReportDetailsAsync(detailData);

        await _reportRepository.UpdateReportStatusAsync(createEvent.ReportId, Report.ReportStatus.Completed);
    }
}