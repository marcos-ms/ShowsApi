using System.Text.Json.Serialization;

namespace SolucionApi.ApiExternas.TvMazeApi.Models;

public class Schedule
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("days")]
    public List<string> Days { get; set; }
}