using Auth.Domain.Entities;
using Domain.Entities;

namespace Auth.Domain.Interfaces.UsuarioInterfaces
{
    public interface IAuthService
    {
        string Logar(string login, string senha);
        Task<Usuario> RegistrarAsync(Usuario usuario);
        Usuario? GetUsuarioLogado();
    }
}
