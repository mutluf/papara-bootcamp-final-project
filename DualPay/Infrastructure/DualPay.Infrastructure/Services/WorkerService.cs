using DualPay.Application.Abstraction;
using DualPay.Infrastructure.Messaging.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace DualPay.Infrastructure;

public class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly ILogger<PaymentCompletedConsumer> _consumerLogger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly PaymentCompletedConsumer _consumer;

    public WorkerService(
        ILogger<WorkerService> logger,
        ILogger<PaymentCompletedConsumer> consumerLogger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _consumerLogger = consumerLogger;
        _serviceScopeFactory = serviceScopeFactory;
        _consumer = new PaymentCompletedConsumer(_consumerLogger,_serviceScopeFactory);
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

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
