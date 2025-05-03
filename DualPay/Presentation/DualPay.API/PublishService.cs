using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DualPay.API;

public class PublishService
{
    public async Task Publish(EventDto eventDto)
    {
        ConnectionFactory factory = new();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";

        await using IConnection connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "queue-test", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var message = JsonConvert.SerializeObject(eventDto);
        var body = Encoding.UTF8.GetBytes(message);
   
        await channel.BasicPublishAsync(exchange: "", routingKey: "queue-test", body: body);

           
    }
}

public class EventDto
{
    public int Id { get; set; }
}