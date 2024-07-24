
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SolucionApi.Data;
using System.Reflection;
using SolucionApi.Filters;

namespace SolucionApi.Swagger;

public static class BuilserServicesExtension
{
    public static void RegisterSwagger(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = builderConfiguration["Swagger:title"],
                Description = builderConfiguration["Swagger:description"],
                License = new OpenApiLicense
                {
                    Name = builderConfiguration["Swagger:license:name"],
                },
                Contact = new OpenApiContact
                {
                    Name = builderConfiguration["Swagger:contact:name"],
                    Email = builderConfiguration["Swagger:contact:email"],
                    Url = new Uri(builderConfiguration["Swagger:contact:url"])  
                }
            });

            // Obtiene la ruta del archivo de comentarios XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // Incluye los comentarios XML
            config.IncludeXmlComments(xmlPath);

            config.OperationFilter<ApiKeyAuthHeaderAttribute>();

        });
    }
}