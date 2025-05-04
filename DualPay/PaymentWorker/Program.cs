using PaymentWorker;
using PaymentWorker.Consumers;
using PaymentWorker.Messaging;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<ExpenseApprovedConsumer>();
builder.Services.AddScoped(typeof(IEventPublishService), typeof(RabbitMqPublishService));
var host = builder.Build();
host.Run();