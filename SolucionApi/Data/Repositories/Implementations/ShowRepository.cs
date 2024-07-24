using SolucionApi.Data.Entities;
using SolucionApi.Data.Repositories.Contracts;

namespace SolucionApi.Data.Repositories.Implementations;

public class ShowRepository : GenericRepository<ShowEntity, string>, IShowRepository
{
    public ShowRepository(DataBaseApiDbContext context) : base(context)
    {
    }
}