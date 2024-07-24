using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SolucionApi.Shared;

namespace SolucionApi.ActionFilters;

public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string APIKEYNAME = GenericMessages.ApiKeyName;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = GenericMessages.NoApiKey
            };
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>(APIKEYNAME);

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = GenericMessages.InvalidApiKey
            };
            return;
        }

        await next();
    }
}

public class ApiKeyAndUserAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string APIKEYNAME = GenericMessages.ApiKeyName;
    private const string USERNAMEHEADER = GenericMessages.ApiKeyUser; 
    private const string USERSLIST = GenericMessages.ApiKeySection; 

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey) ||
            !context.HttpContext.Request.Headers.TryGetValue(USERNAMEHEADER, out var extractedUsername))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = GenericMessages.NoApiKey
            };
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKeys = appSettings.GetSection(USERSLIST).Get<Dictionary<string, string>>();

        if (!apiKeys.TryGetValue(extractedUsername, out var validApiKey) || !validApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = GenericMessages.InvalidApiKey
            };
            return;
        }

        await next();
    }
}

