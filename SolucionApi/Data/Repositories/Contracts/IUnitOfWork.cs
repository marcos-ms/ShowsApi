namespace SolucionApi.Data.Repositories.Contracts;

public interface IUnitOfWork : IDisposable
{
    IShowRepository ShowRepository { get; }
    INetworkRepository NetworkRepository { get; }
    IWebChannelRepository WebChannelRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}