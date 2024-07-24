using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class WebChannel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("country")]
    public Country? Country { get; set; }

    [JsonPropertyName("officialSite")]
    public Uri? OfficialSite { get; set; }
}