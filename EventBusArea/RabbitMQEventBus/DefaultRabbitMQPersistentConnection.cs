using RabbitMQ.Client;

namespace RabbitMQEventBus;

public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
{
    public bool IsConnected => throw new NotImplementedException();

    public IModel CreateModel()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool TryConnect()
    {
        throw new NotImplementedException();
    }
}