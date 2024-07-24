using SolucionApi.Filters;

namespace SolucionApi.Middlewares;

public class CustomHeaderValidatorMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _headerName;

    public CustomHeaderValidatorMiddleware(RequestDelegate next, string headerName)
    {
        _next = next;
        _headerName = headerName;
    }

    public async Task Invoke(HttpContext context)
    {
        if (IsHeaderValidated(context))
        {
            await _next.Invoke(context);

        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Headers are missing");
        }
    }

    private bool IsHeaderValidated(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        if (endpoint is null)
            return true;

        bool isRequired = IsHeaderRequired(endpoint);
        if (!isRequired)
            return true;

        bool isIncluded = IsHeaderIncluded(context);
        if (isRequired && isIncluded)
            return true;

        return false;
    }

    private static bool IsHeaderRequired(Endpoint endpoint)
    {
        var attribute = endpoint.Metadata.GetMetadata<ICustomAttribute>();

        return attribute is { IsMandatory: true };
    }
    private bool IsHeaderIncluded(HttpContext context)
       => context.Request.Headers.Keys.Select(a => a.ToLower()).Contains(_headerName.ToLower());
}