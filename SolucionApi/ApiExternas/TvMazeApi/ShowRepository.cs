using System.Text.Json;
using SolucionApi.ApiExternas.TvMazeApi.Models;

namespace SolucionApi.ApiExternas.TvMazeApi;

public class ShowRepository : IShowRepository
{
    private readonly HttpClient _httpClient;

    public ShowRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(ApiRoutesConst.NameHttpClient);
    }

    public async Task<List<Show>> GetShowsAsync()
    {
        var response = await _httpClient.GetAsync($"{ApiRoutesConst.ShowBaseUrl}");
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        var shows = JsonSerializer.Deserialize<List<Show>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return shows ?? []; 
    }

    public async Task<Show?> GetShowAsync(int showId)
    {
        var response = await _httpClient.GetAsync($"{ApiRoutesConst.ShowBaseUrl}/{showId}");
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        var show = JsonSerializer.Deserialize<Show>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return show;
    }

}