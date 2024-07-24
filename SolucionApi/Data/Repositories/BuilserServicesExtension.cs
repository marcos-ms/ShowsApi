using SolucionApi.Data.Repositories.Contracts;
using SolucionApi.Data.Repositories.Implementations;

namespace SolucionApi.Data.Repositories;

public static class BuilserServicesExtension
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IShowRepository, ShowRepository>();
        services.AddScoped<INetworkRepository, NetworkRepository>();
        services.AddScoped<IWebChannelRepository, WebChannelRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}