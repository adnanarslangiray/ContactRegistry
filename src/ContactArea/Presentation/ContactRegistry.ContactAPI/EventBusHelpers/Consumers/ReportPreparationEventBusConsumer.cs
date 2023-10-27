using ContantRegistry.Application.Abstractions.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventBus;
using RabbitMQEventBus.Constants;
using RabbitMQEventBus.Events;
using RabbitMQEventBus.Producer;
using System.Text;

namespace ContactRegistry.ContactAPI.EventBusHelpers.Consumers;

public class ReportPreparationEventBusConsumer
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly IContactService _contactService;
    private readonly IRabbitMQEventBusProducer _eventBus;

    public ReportPreparationEventBusConsumer(IRabbitMQPersistentConnection persistentConnection, IContactService contactService, IRabbitMQEventBusProducer eventBus)
    {
        _persistentConnection = persistentConnection;
        _contactService = contactService;
        _eventBus = eventBus;
    }

    public void Consume()
    {
        if (_persistentConnection.IsConnected == false)
            _persistentConnection.TryConnect();

        var channel = _persistentConnection.CreateModel();
        channel.QueueDeclare(queue: EventConstants.ContactReportQueue, durable: true, false, false, null);
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += Consumer_Received;
        channel.BasicConsume(EventConstants.ContactReportQueue, true, consumer);
    }

    private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var @reportCreateEvent = JsonConvert.DeserializeObject<ReportRequestEvent>(message);
        if (e.RoutingKey == EventConstants.ContactReportQueue)
        {
            await CreateReport(@reportCreateEvent.ReportId);
        }
    }

    public void Disconnect()
    {
        _persistentConnection.Dispose();
    }

    public async Task CreateReport(string reportId)
    {
        var result = await _contactService.PrepareContactReport();
        result.ReportId = reportId;

        if (result is not null)
        {
            try
            {
                ReportCreateEvent createReportEvent = new()
                {
                    ReportId = result.ReportId,
                    ReportDetails = result.ReportDetails.Select(x => new ReportDetailDetailsBus()
                    {
                        ContactCount = x.ContactCount,
                        Location = x.Location,
                        PhoneNumberCount = x.PhoneNumberCount
                    }).ToList()
                };

                _eventBus.Publish(EventConstants.ContactReportCreateQueue, createReportEvent);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}