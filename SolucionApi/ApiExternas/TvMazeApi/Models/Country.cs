using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class Country
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }
}