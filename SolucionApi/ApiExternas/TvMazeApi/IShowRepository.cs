using SolucionApi.ApiExternas.TvMazeApi.Models;

namespace SolucionApi.ApiExternas.TvMazeApi;

public interface IShowRepository
{
    Task<List<Show>> GetShowsAsync();
    Task<Show?> GetShowAsync(int showId);
}