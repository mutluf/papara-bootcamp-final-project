using Microsoft.Extensions.Configuration;
namespace DualPay.Persistence;

static class Configuration
{
    static public string ConnectionString
    {
        get {
            ConfigurationManager cfg = new();
            cfg.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DualPay.API"));
            cfg.AddJsonFile("appsettings.json");

            return cfg.GetConnectionString("MicrosoftSQL");
        }
    }
}