namespace SolucionApi.Data.Entities;

public class ShowEntity : Entity<string>
{
    public Uri? Url { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? Language { get; set; }

    public List<string> Genres { get; set; } = new();

    public string? Status { get; set; }

    public int? Runtime { get; set; }

    public int? AverageRuntime { get; set; }

    public DateTime? Premiered { get; set; }

    public DateTime? Ended { get; set; }

    public Uri? OfficialSite { get; set; }

    public ScheduleEntity? Schedule { get; set; }

    public RatingEntity Rating { get; set; }

    public int Weight { get; set; }

    public CountryEntity DvdCountry { get; set; }

    public ExternalsEntity Externals { get; set; }

    public ImageEntity Image { get; set; }

    public string? Summary { get; set; }

    public long? Updated { get; set; }

    public LinksEntity Links { get; set; }


    public string? NetworkId { get; set; }
    public NetworkEntity? Network { get; set; }

    public string? WebChannelId { get; set; }
    public WebChannelEntity? WebChannel { get; set; }

}