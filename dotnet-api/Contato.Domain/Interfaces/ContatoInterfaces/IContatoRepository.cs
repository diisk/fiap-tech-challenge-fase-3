using Domain.Entities;
using Domain.Interfaces;

namespace Contato.Domain.Interfaces.ContatoInterfaces
{
    public interface IContatoRepository : IRepository<Entities.Contato>
    {
        Entities.Contato? FindByCodigoAreaAndTelefone(int codigoArea, int telefone);
        List<Entities.Contato> FindByCodigoArea(int codigoArea);
    }
}
