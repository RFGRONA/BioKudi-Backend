namespace Biokudi_Backend.Infrastructure.Config
{
    public static class CORSConfig
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllAllowed", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                options.AddPolicy("OnlyFrontend", policy =>
                {
                    policy.WithOrigins("https://biokudi.vercel.app")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });

            });

            return services;
        }
    }
}
