namespace SolucionApi.Dtos;

public class ShowDto
{
    public string? Id { get; set; }
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
    public ScheduleDto? Schedule { get; set; }
    public RatingDto? Rating { get; set; }
    public int Weight { get; set; }
    public NetworkDto? Network { get; set; }
    public WebChannelDto? WebChannel { get; set; }
    public CountryDto? DvdCountry { get; set; }
    public ExternalsDto? Externals { get; set; }
    public ImageDto? Image { get; set; }
    public string? Summary { get; set; }
    public long? Updated { get; set; }
    public LinksDto? Links { get; set; }
}