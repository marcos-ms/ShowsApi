using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;

namespace SolucionApi.Data.Repositories.Implementations;

public class WebChannelRepository : GenericRepository<WebChannelEntity, string>, IWebChannelRepository
{
    public WebChannelRepository(DataBaseApiDbContext context) : base(context)
    {
    }
}