namespace SolucionApi.Dtos;

public class NetworkDto
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public CountryDto? Country { get; set; }
    public Uri? OfficialSite { get; set; }
}