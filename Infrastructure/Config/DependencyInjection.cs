using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Services;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Repositories;
using Biokudi_Backend.Infrastructure.Services;

namespace Biokudi_Backend.Infrastructure.Config
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Application layer (AddScoped)
            services.AddScoped<PlaceMapping>();
            services.AddScoped<EmailUtility>();
            services.AddSingleton<RSAUtility>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPlaceService, PlaceService>();

            // Infrastructure layer (AddScoped)
            services.AddScoped<CaptchaService>();
            services.AddScoped<CookiesService>();
            services.AddScoped<AuthService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();

            // Ui layer (Transient)

        }
    }
}
