using Area.Domain.Interfaces.AreaInterfaces;
using Area.Infrastructure.DbContexts;
using Infrastructure.Repositories;

namespace Area.Infrastructure.Repositories
{
    public class AreaRepository : BaseRepository<Domain.Entities.Area>, IAreaRepository
    {
        public AreaRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext) { }

        public List<Domain.Entities.Area> FindByCodigo(List<int> codigos)
        {
            return onlyReadDbSet.Where(x => codigos.Contains(x.Codigo)).ToList();
        }
    }
}
