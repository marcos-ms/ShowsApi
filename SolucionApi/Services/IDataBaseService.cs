using SolucionApi.Dtos;
using SolucionApi.Shared;

namespace SolucionApi.Services;

public interface IDataBaseService : IReader<ShowDto, string>
{
    Task<List<ShowDto>> GetShowsAsync(ShowFilter filter);
    List<ShowDto> GetShows(ShowFilter filter);
    Task<ResultValue> SaveShowImportsAsync(List<ShowDto> shows);
    Task<ResultValue> SaveShowAsync(ShowDto show);
    Task<ResultValue<ShowDto>> SaveShowsAsync(ShowDto entidad);
    Task<ResultValue> UpdateShowsAsync(ShowDto show);
    Task<ResultValue> DeleteShowAsync(string id);
    Task ClearDataBase();
}