using Auth.Domain.Entities;
using Auth.Domain.Interfaces.UsuarioInterfaces;
using Auth.Infrastructure.DbContexts;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Auth.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext) : base(onlyWriteDbContext, onlyReadDbContext) { }

        public Usuario? FindByLogin(string login)
        {
            return onlyReadDbSet.FirstOrDefault(x => x.Login == login);
        }
    }
}
