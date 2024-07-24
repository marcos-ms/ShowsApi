using SolucionApi.Data.Repositories.Contracts;

namespace SolucionApi.Data.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    public IShowRepository ShowRepository => _showRepository;
    public INetworkRepository NetworkRepository => _networkRepository;
    public IWebChannelRepository WebChannelRepository => _webChannelRepository;

    private readonly DataBaseApiDbContext _context;
    private readonly IShowRepository _showRepository;
    private readonly INetworkRepository _networkRepository;
    private readonly IWebChannelRepository _webChannelRepository;

    public UnitOfWork(DataBaseApiDbContext context,
        IShowRepository showRepository,
        INetworkRepository networkRepository,
        IWebChannelRepository webChannelRepository)
    {
        _context = context;
        _showRepository = showRepository;
        _networkRepository = networkRepository;
        _webChannelRepository = webChannelRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}