using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace PaymentWorker.Messaging;

public class RabbitMqPublishService : IEventPublishService
{
    public async Task PublishAsync<T>(T @event) where T : IApplicationEvent
    {
        var rabbitMq = Configuration.RabbitMqSettings;
        
        ConnectionFactory factory = new();
        factory.HostName = rabbitMq.Host;
        factory.Port = rabbitMq.Port;
        factory.UserName = rabbitMq.UserName;
        factory.Password = rabbitMq.Password;

        await using IConnection connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        var queueName = typeof(T).Name; 
        
        await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false,arguments: null);
        var message = SerializeEvent(@event);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        Console.WriteLine($"Event published: {queueName}");
    }
    
    private string SerializeEvent<T>(T @event) where T : IApplicationEvent
    {
        return JsonConvert.SerializeObject(@event);
    }
}