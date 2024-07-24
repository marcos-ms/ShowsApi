using SolucionApi.Services.Implementations;

namespace SolucionApi.Services;

public static class BuilserServicesExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDataBaseService, DataBaseService>();
        services.AddScoped<ITvMazeService, TvMazeService>();
    }
}