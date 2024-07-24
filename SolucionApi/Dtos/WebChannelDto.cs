namespace SolucionApi.Dtos;

public class WebChannelDto
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public CountryDto? Country { get; set; }
    public Uri? OfficialSite { get; set; }
}