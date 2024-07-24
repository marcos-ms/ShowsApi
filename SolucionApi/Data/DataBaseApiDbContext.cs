using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SolucionApi.Data.Entities;

namespace SolucionApi.Data;

public class DataBaseApiDbContext : DbContext
{
    public DataBaseApiDbContext(DbContextOptions<DataBaseApiDbContext> options) : base(options)
    {
    }

    public DbSet<ShowEntity> Shows { get; set; }
    public DbSet<NetworkEntity> Networks { get; set; }
    public DbSet<WebChannelEntity> WebChannels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetDefaultValues();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDefaultValues();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDefaultValues()
    {
        var entries = ChangeTracker.Entries<ShowEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            entry.Entity.Updated = ToUnixTime(DateTime.UtcNow);
        }
    }

    // Desde  una fecha a una fecha unix
    private long ToUnixTime(DateTime date) => new DateTimeOffset(date).ToUnixTimeSeconds();
}