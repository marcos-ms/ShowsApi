using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class Self
{
    [JsonPropertyName("href")]
    public Uri Href { get; set; }
}