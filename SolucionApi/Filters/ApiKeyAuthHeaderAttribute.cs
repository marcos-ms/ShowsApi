using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SolucionApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthHeaderAttribute : Attribute, IOperationFilter, ICustomAttribute
{
    public static string ApiKeyHeaderName = "Shows-ApiKey";
    public static string UserHeaderName = "Shows-User";
    public bool IsMandatory { get; }

    public ApiKeyAuthHeaderAttribute(bool isMandatory = false)
    {
        IsMandatory = isMandatory;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        CustomAttribute attribute = context.RequiredAttribute<ApiKeyAuthHeaderAttribute>();

        if (!attribute.ContainsAttribute)
            return;

        operation.Parameters.Add(
            new OpenApiParameter()
            {
                Name = UserHeaderName,
                In = ParameterLocation.Header,
                Description = "Shows API user",
                Required = attribute.Mandatory,
                Schema = new OpenApiSchema() { Type = "String" }
            });
        operation.Parameters.Add(
            new OpenApiParameter()
            {
                Name = ApiKeyHeaderName,
                In = ParameterLocation.Header,
                Description = "Shows API Key",
                Required = attribute.Mandatory,
                Schema = new OpenApiSchema() { Type = "String" }
            });
    }
}