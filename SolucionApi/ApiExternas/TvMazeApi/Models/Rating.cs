using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class Rating
{
    [JsonPropertyName("average")]
    public double? Average { get; set; }
}