namespace SolucionApi.Services;

public interface IReader<T,TId>
{
    Task<List<T>> GetShowsAsync();
    Task<T?> GetShowAsync(TId showId);
}