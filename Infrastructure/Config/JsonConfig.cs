using System.Text.Json;
using System.Text.Json.Serialization;

namespace Biokudi_Backend.Infrastructure.Config
{
    public static class JsonConfig
    {
        public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = false;
                });
            return services;
        }
    }
}
