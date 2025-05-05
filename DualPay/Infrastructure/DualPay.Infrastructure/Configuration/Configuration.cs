using Microsoft.Extensions.Configuration;

namespace DualPay.Infrastructure;

public static class Configuration
{
    public static RabbitMQOptions RabbitMqSettings
    {
        get
        {
            var cfg = new ConfigurationManager();
            cfg.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DualPay.API"));
            cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return cfg.GetSection("RabbitMQ").Get<RabbitMQOptions>()!;
        }
    }
}