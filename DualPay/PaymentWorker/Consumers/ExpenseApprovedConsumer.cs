using System.Text;
using Newtonsoft.Json;
using PaymentWorker.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PaymentWorker.Consumers;

public class ExpenseApprovedConsumer
{
    private readonly ILogger<ExpenseApprovedConsumer> _logger;
    public ExpenseApprovedConsumer(ILogger<ExpenseApprovedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task ConsumeAsync(IChannel channel)
    {
        await channel.QueueDeclareAsync(
            queue: nameof(ExpenseApprovedEvent),
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        await channel.QueueDeclareAsync(
            queue: nameof(PaymentCompletedEvent),
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var jsonData = Encoding.UTF8.GetString(body);

            try
            {
                var expense = JsonConvert.DeserializeObject<ExpenseApprovedEvent>(jsonData);
                Console.WriteLine($"Received Message: {expense?.Amount}");

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                
                await Task.Delay(TimeSpan.FromSeconds(10));
                
                var paymentCompletedEvent = new PaymentCompletedEvent
                {
                    Amount = expense.Amount,
                    ToAccount = expense.ToAccount,
                    FromAccount = expense.FromAccount,
                    Status = "Completed",
                    CompletedAt = DateTime.UtcNow,
                    ExpenseId = expense.ExpenseId
                };

                var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(paymentCompletedEvent));

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "PaymentCompletedEvent",
                    body: messageBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred.");
                await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        await channel.BasicConsumeAsync(
            queue: "ExpenseApprovedEvent",
            autoAck: false,
            consumer: consumer);
    }
}