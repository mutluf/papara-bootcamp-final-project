using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQProducerConsumer.RabbitMq;

public class Consumer
{
    public async void Register()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "queue-test", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray(); // 🔧 Yeni RabbitMQ versiyonları için ToArray() kullan
            var jsonData = Encoding.UTF8.GetString(body);
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonData);

            Console.WriteLine($"Gelen Mesaj: {eventDto.Id}");

            // Mesaj işlendi olarak işaretle
            channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            return Task.CompletedTask;
        };

       await  channel.BasicConsumeAsync(queue: "queue-test", autoAck: false, consumer: consumer);

        Console.WriteLine("Dinleme başlatıldı. Çıkmak için bir tuşa basın...");
        Console.ReadLine();
    }
}

public class EventDto
{
    public int Id { get; set; }
}