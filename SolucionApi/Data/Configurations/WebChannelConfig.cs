using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolucionApi.Data.Entities;

namespace SolucionApi.Data.Configurations;

public class WebChannelConfig : IEntityTypeConfiguration<WebChannelEntity>
{
    public void Configure(EntityTypeBuilder<WebChannelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasMaxLength(36).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.OwnsOne(x => x.Country, y =>
        {
            y.Property(b => b.Name).HasColumnName("CountryName").HasMaxLength(256);
            y.Property(b => b.Code).HasColumnName("CountryCode").HasMaxLength(10);
            y.Property(b => b.Timezone).HasColumnName("CountryTimeZone").HasMaxLength(128);
        });
        builder.Property(x => x.OfficialSite).HasConversion<UriToStringConverter>().HasMaxLength(512);
    }
}