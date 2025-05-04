using System.Text;
using DualPay.Application.Abstraction;
using DualPay.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DualPay.Infrastructure.Messaging.Consumers;
public class PaymentCompletedConsumer
{
    private readonly ILogger<PaymentCompletedConsumer> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public PaymentCompletedConsumer(ILogger<PaymentCompletedConsumer> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task ConsumeAsync(IChannel channel)
    {
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
                var payment = JsonConvert.DeserializeObject<PaymentCompletedEvent>(jsonData);
                Console.WriteLine($"Payment Status: {payment?.Status}");

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userService = scope.ServiceProvider.GetRequiredService<IAppUserService>();

                    if (payment.Status == "Completed")
                    {
                        await _userService.TransferBalanceAsync(payment.FromAccount, payment.ToAccount, payment.Amount);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred.");
                await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        await channel.BasicConsumeAsync(
            queue: nameof(PaymentCompletedEvent),
            autoAck: false,
            consumer: consumer);
    }
}