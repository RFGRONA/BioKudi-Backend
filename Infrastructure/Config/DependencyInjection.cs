using Biokudi_Backend.Application.Interfaces;
using Biokudi_Backend.Application.Mappings;
using Biokudi_Backend.Application.Services;
using Biokudi_Backend.Application.Utilities;
using Biokudi_Backend.Domain.Interfaces;
using Biokudi_Backend.Infrastructure.Repositories;
using Biokudi_Backend.Infrastructure.Services;

namespace Biokudi_Backend.Infrastructure.Config
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure layer (AddScoped)
            services.AddScoped<CaptchaService>();
            services.AddScoped<CookiesService>();
            services.AddSingleton<AuthService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<SanitizerService>();

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();

            // Application layer (AddScoped)
            services.AddSingleton<RSAUtility>();
            services.AddScoped<EmailUtility>();

            services.AddScoped<PersonMapping>();
            services.AddScoped<PlaceMapping>();
            services.AddScoped<CityMapping>();
            services.AddScoped<DepartmentMapping>();
            services.AddScoped<StateMapping>();
            services.AddScoped<ActivityMapping>();
            services.AddScoped<TypeMapping>();
            services.AddScoped<PictureMapping>();
            services.AddScoped<RoleMapping>();
            services.AddScoped<ReviewMapping>();
            services.AddScoped<TicketMapping>();
            services.AddScoped<AuditMapping>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<ITableRelationService, TableRelationService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IAuditService, AuditService>();
        }
    }
}
