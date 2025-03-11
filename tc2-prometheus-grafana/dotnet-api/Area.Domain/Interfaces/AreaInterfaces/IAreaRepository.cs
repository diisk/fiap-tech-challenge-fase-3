using Area.Domain.Entities;
using Domain.Interfaces;

namespace Area.Domain.Interfaces.AreaInterfaces
{
    public interface IAreaRepository : IRepository<Entities.Area>
    {
        List<Entities.Area> FindByCodigo(List<int> codigos);
    }
}
