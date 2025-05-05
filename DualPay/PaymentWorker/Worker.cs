using PaymentWorker.Consumers;
using RabbitMQ.Client;

namespace PaymentWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<ExpenseApprovedConsumer> _consumerLogger;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ExpenseApprovedConsumer _consumer;

    public Worker(
        ILogger<ExpenseApprovedConsumer> consumerLogger)
    {
        _consumerLogger = consumerLogger;
        _consumer = new ExpenseApprovedConsumer(_consumerLogger);
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var rabbitMq = Configuration.RabbitMqSettings;
        ConnectionFactory factory = new();
        factory.HostName = rabbitMq.Host;
        factory.Port = rabbitMq.Port;
        factory.UserName = rabbitMq.UserName;
        factory.Password = rabbitMq.Password;

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _consumer.ConsumeAsync(_channel);

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.CloseAsync();
        _connection?.CloseAsync();
        return base.StopAsync(cancellationToken);
    }
}
