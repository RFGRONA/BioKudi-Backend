using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Services;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Repositories;

namespace Biokudi_Backend.Infrastructure.Config
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Applitación layer (AddScoped)
            services.AddScoped<PlaceMapping>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ICacheService, CacheService>();

            // Infrastructure layer (AddScoped)
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();

            // Ui layer (Transient)
            
        }
    }
}
