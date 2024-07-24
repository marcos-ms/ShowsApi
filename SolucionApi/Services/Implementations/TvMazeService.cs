using SolucionApi.ApiExternas.TvMazeApi;
using SolucionApi.Dtos;

namespace SolucionApi.Services.Implementations;

public class TvMazeService : ITvMazeService
{
    private readonly IShowRepository _showRepository;

    public TvMazeService(IShowRepository showRepository)
    {
        this._showRepository = showRepository;
    }

    public async Task<List<ShowDto>> GetShowsAsync()
    {
        try
        {
            var shows = await _showRepository.GetShowsAsync();

            var showDtos = shows
                .Select(x => x.ToDto())
                .ToList();

            return showDtos;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<ShowDto?> GetShowAsync(int showId)
    {
        var show = await _showRepository.GetShowAsync(showId);
        return show?.ToDto() ?? null;
    }
}