using Biokudi_Backend.Infrastructure.Services;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Biokudi_Backend.Infrastructure.Config
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Biokudi API",
                    Version = "v1",
                    Description = "Gestión y validación de datos. Para más detalles, consulta la [documentación completa en GitHub](https://github.com/RFGRONA/BioKudi-Backend/wiki/).",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipo de Soporte Biokudi",
                        Email = "contacto@biokudi.com",
                        Url = new Uri("https://biokudi.site/help")
                    },
                    TermsOfService = new Uri("https://biokudi.site/privacy-policy"),
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        { "x-documentation-url", new OpenApiString("https://github.com/RFGRONA/BioKudi-Backend/wiki/") }
                    }
                });

                options.AddSecurityDefinition("cookieAuth", new OpenApiSecurityScheme
                {
                    Name = "Cookie",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Description = "Autenticación mediante cookies. Debe iniciar sesión y enviar una solicitud con la cookie de autenticación válida."
                });


                options.OperationFilter<ProducesResponseTypeFilter>();
                options.OperationFilter<UnauthorizedResponseFilter>();
                options.OperationFilter<ProducesJsonFilter>();
                options.OperationFilter<AuthOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
