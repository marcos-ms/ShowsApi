
using Microsoft.EntityFrameworkCore;

namespace SolucionApi.Data;

public static class BuilserServicesExtension
{
    public static void RegisterDbContext(this IServiceCollection services, string connectionString)
    {
        if(string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        services.AddDbContext<DataBaseApiDbContext>(options =>
        {
            var migrationsAssembly = typeof(DataBaseApiDbContext).Assembly.GetName().Name;
            options.UseSqlServer(connectionString,
                sqlServer =>
                {
                    sqlServer.MigrationsAssembly(migrationsAssembly);
                });
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}