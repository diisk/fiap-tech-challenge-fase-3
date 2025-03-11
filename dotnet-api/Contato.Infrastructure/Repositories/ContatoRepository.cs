using Contato.Domain.Interfaces.ContatoInterfaces;
using Contato.Infrastructure.DbContexts;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Contato.Infrastructure.Repositories
{
    public class ContatoRepository : BaseRepository<Domain.Entities.Contato>, IContatoRepository
    {
        public ContatoRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext) { }

        public List<Domain.Entities.Contato> FindByCodigoArea(int codigoArea)
        {
            return onlyReadDbSet.Where(c => !c.Removed && c.Area.Codigo == codigoArea).ToList();
        }

        public Domain.Entities.Contato? FindByCodigoAreaAndTelefone(int codigoArea, int telefone)
        {
            return onlyReadDbSet.FirstOrDefault(
                c => c.Telefone == telefone
                && c.CodigoArea == codigoArea
                && !c.Removed);
        }
    }
}
