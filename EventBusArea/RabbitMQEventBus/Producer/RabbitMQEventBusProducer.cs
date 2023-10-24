using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client.Exceptions;
using RabbitMQEventBus.Events.Interfaces;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RabbitMQEventBus.Producer;

public class RabbitMQEventBusProducer
{
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly ILogger<RabbitMQEventBusProducer> _logger;
    private readonly int _retryCount;

    public RabbitMQEventBusProducer(ILogger<RabbitMQEventBusProducer> logger, IRabbitMQPersistentConnection persistentConnection, int retryCount)
    {
        _logger=logger;
        _persistentConnection=persistentConnection;
        _retryCount=retryCount;
    }

    public void Publish(string queueName, IEvent @event)
    {
        if (_persistentConnection.IsConnected == false)
            _persistentConnection.TryConnect();

        //polling policy
        var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex.ToString());
                    });

        _logger.LogInformation("RabbitMQ persistent connection acquired a connection");
        using var channel = _persistentConnection.CreateModel();
        var eventName = @event.GetType().Name;
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        //policy publish için eklendi, eğer publish başarısız olursa tekrar dener
        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2; // persistent
            channel.ConfirmSelect();
            channel.BasicPublish(exchange: "", routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
            channel.WaitForConfirmsOrDie();
            channel.BasicAcks += (sender, eventArgs) =>
            {
                _logger.LogInformation("Sent RabbitMQ");
            };
            channel.ConfirmSelect();
        });
    }
}