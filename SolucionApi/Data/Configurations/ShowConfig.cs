using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SolucionApi.Data.Converters;
using SolucionApi.Data.Entities;

namespace SolucionApi.Data.Configurations;

public class ShowConfig : IEntityTypeConfiguration<ShowEntity>
{
    public void Configure(EntityTypeBuilder<ShowEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasMaxLength(36).IsRequired();
        builder.Property(x => x.Url)
            .HasConversion<UriToStringConverter>()
            .HasMaxLength(512);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Type).HasMaxLength(64);
        builder.Property(x => x.Language).HasMaxLength(64);
        builder.Property(x => x.Genres)
            .HasConversion(new StringListConverter())
            .HasMaxLength(512);
        builder.Property(x => x.Status).HasMaxLength(64);
        builder.Property(x => x.Premiered).HasColumnType("date");
        builder.Property(x => x.Ended).HasColumnType("date");
        builder.Property(x => x.OfficialSite).IsRequired(false);
        builder.OwnsOne(x => x.Schedule, y =>
        {
            y.Property(b => b.Time).HasColumnName("Time")
                .HasMaxLength(64);
            y.Property(b => b.Days).HasColumnName("Days")
                .HasConversion(new StringListConverter())
                .HasMaxLength(64);
        });
        builder.OwnsOne(x => x.Rating, y =>
        {
            y.Property(b => b.Average).HasColumnName("Average").HasPrecision(8,2);
        });
        builder.OwnsOne(x => x.DvdCountry, y =>
        {
            y.Property(b => b.Name).IsRequired(false).HasColumnName("DvdCountryName").HasMaxLength(256);
            y.Property(b => b.Code).HasColumnName("DvdCountryCode").HasMaxLength(10);
            y.Property(b => b.Timezone).HasColumnName("DvdCountryTimeZone").HasMaxLength(128);
        });
        builder.OwnsOne(x => x.Externals, y =>
        {
            y.Property(b => b.Tvrage).HasColumnName("Tvrage").HasMaxLength(64);
            y.Property(b => b.Thetvdb).HasColumnName("Thetvdb").HasMaxLength(64);
            y.Property(b => b.Imdb).HasColumnName("Imdb").HasMaxLength(64);
        });
        builder.OwnsOne(x => x.Image, y =>
        {
            y.Property(b => b.Medium).HasColumnName("Medium").HasMaxLength(512);
            y.Property(b => b.Original).HasColumnName("Original").HasMaxLength(512);
        });
        builder.OwnsOne(x => x.Links, l =>
        {
            l.OwnsOne(links => links.Self, s =>
            {
                s.Property(self => self.Href)
                    .HasColumnName("SelfHref");
            });

            l.OwnsOne(links => links.Previousepisode, pe =>
            {
                pe.Property(ep => ep.Href)
                    .HasColumnName("PreviousEpisodeHref");
                pe.Property(ep => ep.Name)
                    .HasColumnName("PreviousEpisodeName")
                    .IsRequired(false)
                    .HasMaxLength(256);
            });

            l.OwnsOne(links => links.Nextepisode, ne =>
            {
                ne.Property(ep => ep.Href)
                    .HasColumnName("NextEpisodeHref");
                ne.Property(ep => ep.Name)
                    .HasColumnName("NextEpisodeName")
                    .IsRequired(false)
                    .HasMaxLength(256);
            });
        });
        
        builder.Property(x => x.Summary).HasMaxLength(4096);

        builder.HasOne(e => e.Network)
            .WithMany(n => n.Shows)
            .HasForeignKey(e => e.NetworkId)
            .IsRequired(false);

        builder.HasOne(e => e.WebChannel)
            .WithMany(n => n.Shows)
            .HasForeignKey(e => e.WebChannelId)
            .IsRequired(false);
    }

}