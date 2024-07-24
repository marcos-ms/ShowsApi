using Swashbuckle.AspNetCore.SwaggerGen;

namespace SolucionApi.Filters;

public static class OperationFilterContextExtensions
{
    public static CustomAttribute RequiredAttribute<T>(this OperationFilterContext context) where T : ICustomAttribute
    {
        var globalAttributes = context
            .ApiDescription
            .ActionDescriptor
            .FilterDescriptors
            .Select(p => p.Filter);

        var controllerAttributes = context
            .MethodInfo?
            .DeclaringType?
            .GetCustomAttributes(true) ?? [];

        var methodAttributes = context
            .MethodInfo?
            .GetCustomAttributes(true) ?? [];

        List<T> containsAttribute = globalAttributes
            .Union(controllerAttributes)
            .Union(methodAttributes)
            .OfType<T>()
            .ToList();

        return containsAttribute.Count == 0
            ? new CustomAttribute(false, false)
            : new CustomAttribute(true, containsAttribute.First().IsMandatory);
    }
}