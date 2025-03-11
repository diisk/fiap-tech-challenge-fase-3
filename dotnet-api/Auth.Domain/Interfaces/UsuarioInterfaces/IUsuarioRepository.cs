using Auth.Domain.Entities;
using Domain.Entities;
using Domain.Interfaces;

namespace Auth.Domain.Interfaces.UsuarioInterfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario? FindByLogin(string email);
    }
}
