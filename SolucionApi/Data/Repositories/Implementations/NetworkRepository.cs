using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;

namespace SolucionApi.Data.Repositories.Implementations;

public class NetworkRepository : GenericRepository<NetworkEntity, string>, INetworkRepository
{
    public NetworkRepository(DataBaseApiDbContext context) : base(context)
    {
    }
}