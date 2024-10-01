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
            // Infrastructure layer (AddScoped)
            services.AddScoped<CaptchaService>();
            services.AddScoped<CookiesService>();
            services.AddSingleton<AuthService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();

            // Application layer (AddScoped)
            services.AddScoped<PlaceMapping>();
            services.AddScoped<CityMapping>();
            services.AddScoped<DepartmentMapping>();
            services.AddScoped<StateMapping>();
            services.AddScoped<ActivityMapping>();
            services.AddSingleton<RSAUtility>();
            services.AddScoped<EmailUtility>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IStateService, StateService>();
        }
    }
}
