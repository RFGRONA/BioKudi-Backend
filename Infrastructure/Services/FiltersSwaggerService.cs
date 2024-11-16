using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Biokudi_Backend.Infrastructure.Services
{
    public class ProducesResponseTypeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.TryAdd("400", new OpenApiResponse { Description = "Bad Request" });
            operation.Responses.TryAdd("404", new OpenApiResponse { Description = "Not Found" });
            operation.Responses.TryAdd("429", new OpenApiResponse { Description = "Too Many Requests" });
            operation.Responses.TryAdd("500", new OpenApiResponse { Description = "Internal Server Error" });
        }
    }
    public class UnauthorizedResponseFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                  .Union(context.MethodInfo.GetCustomAttributes(true))
                                  .Any(attr => attr.GetType().Name == "AuthorizeAttribute");

            if (hasAuthorize)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            }
        }
    }
    public class ProducesJsonFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var returnType = context.MethodInfo.ReturnType;
            Type actualReturnType = null;

            var produceResponseTypeAttribute = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<ProducesResponseTypeAttribute>()
                .FirstOrDefault(a => a.StatusCode == StatusCodes.Status200OK);

            if (produceResponseTypeAttribute != null && produceResponseTypeAttribute.Type != null)
            {
                actualReturnType = produceResponseTypeAttribute.Type;
            }
            else if (returnType != null)
            {
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    var taskType = returnType.GetGenericArguments()[0];
                    if (typeof(IActionResult).IsAssignableFrom(taskType))
                    {
                        var methodAttributes = context.MethodInfo.GetCustomAttributes<ProducesResponseTypeAttribute>();
                        var okResponseType = methodAttributes.FirstOrDefault(a => a.StatusCode == StatusCodes.Status200OK);
                        if (okResponseType != null)
                        {
                            actualReturnType = okResponseType.Type;
                        }
                    }
                    else
                    {
                        actualReturnType = taskType;
                    }
                }
                else if (typeof(IActionResult).IsAssignableFrom(returnType))
                {
                    var methodAttributes = context.MethodInfo.GetCustomAttributes<ProducesResponseTypeAttribute>();
                    var okResponseType = methodAttributes.FirstOrDefault(a => a.StatusCode == StatusCodes.Status200OK);
                    if (okResponseType != null)
                    {
                        actualReturnType = okResponseType.Type;
                    }
                }
                else
                {
                    actualReturnType = returnType;
                }
            }

            if (actualReturnType != null)
            {
                if (!operation.Responses.ContainsKey("200"))
                {
                    operation.Responses.Add("200", new OpenApiResponse());
                }

                var response = operation.Responses["200"];

                var schema = context.SchemaGenerator.GenerateSchema(actualReturnType, context.SchemaRepository);

                response.Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = schema
                    }
                };
            }
        }
    }

    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                   .OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.GetCustomAttributes(true)
                                   .OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "cookieAuth"
                            }
                        },
                        new string[] { }
                    }
                }
            };
            }
        }
    }
}