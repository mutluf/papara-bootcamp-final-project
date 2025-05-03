using PaymentWorker;
using RabbitMQProducerConsumer.RabbitMq;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<Consumer>();
var host = builder.Build();
host.Run();