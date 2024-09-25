namespace Biokudi_Backend.Infrastructure.Config
{
    public static class CORSConfig
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllAllowed", builder =>
                {
                    var allowedOrigins = new[]
                    {
                        "http://localhost:3000",
                        "https://fredybk.vercel.app",
                        "https://biokudi.vercel.app",
                        "https://biokudi.site"
                    };

                    builder.WithOrigins(allowedOrigins) 
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
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
