
namespace SolucionApi.ApiExternas.TvMazeApi;

public static class BuilserServicesExtension
{
    public static void RegisterTvMaziApi(this IServiceCollection services, string tvMazeApiUrl)
    {
        if (string.IsNullOrEmpty(tvMazeApiUrl))
        {
            throw new ArgumentNullException(nameof(tvMazeApiUrl));
        }

        services.AddHttpClient(ApiRoutesConst.NameHttpClient, client =>
        {
            client.BaseAddress = new Uri(tvMazeApiUrl);
        });

        services.AddScoped<IShowRepository, ShowRepository>();
    }
}