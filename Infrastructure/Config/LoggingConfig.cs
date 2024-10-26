using Serilog;
using Serilog.Sinks.Http;
using Serilog.Sinks.Http.BatchFormatters;
using System.Net.Http;

namespace Biokudi_Backend.Infrastructure.Config
{
    public static class LoggingConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var betterStackToken = configuration["Logging:BetterStack:ApiToken"];

            var authenticatedHttpClient = new AuthenticatedHttpClient(betterStackToken);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "Logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 1
                )
                .WriteTo.Http(
                    requestUri: "https://in.logs.betterstack.com",
                    queueLimitBytes: 10000,
                    httpClient: authenticatedHttpClient,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    batchFormatter: new ArrayBatchFormatter()
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

    public class AuthenticatedHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public AuthenticatedHttpClient(string token)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public void Configure(IConfiguration configuration)
        {

        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return _httpClient.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream, CancellationToken cancellationToken)
        {
            using var content = new StreamContent(contentStream);
            return await _httpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
