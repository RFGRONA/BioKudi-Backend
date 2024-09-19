using Serilog;

namespace Biokudi_Backend.Infrastructure.Config
{
    public class LoggingConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "Logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 1
                )
                .CreateLogger();
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (Directory.Exists(logDirectory))
            {
                var files = Directory.GetFiles(logDirectory, "*.txt");
                foreach (var file in files)
                {
                    var creationDate = File.GetCreationTime(file);
                    if (creationDate < DateTime.Now.Date)
                    {
                        File.Delete(file);
                    }
                }

            }
        }
    }
}
