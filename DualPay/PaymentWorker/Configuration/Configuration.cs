namespace PaymentWorker;

public static class Configuration
{
    public static RabbitMQOptions RabbitMqSettings
    {
        get
        {
            var cfg = new ConfigurationManager();
            cfg.SetBasePath(AppContext.BaseDirectory);
            cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var section = cfg.GetSection("RabbitMQ");
            var options = cfg.GetSection("RabbitMQ").Get<RabbitMQOptions>();
            return options;
        }
    }
}