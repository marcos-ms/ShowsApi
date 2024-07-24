using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class Image
{
    [JsonPropertyName("medium")]
    public Uri? Medium { get; set; }

    [JsonPropertyName("original")]
    public Uri? Original { get; set; }
}