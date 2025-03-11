using Domain.Exceptions;
using System.Net;

namespace Contato.Domain.Exceptions.ContatoExceptions
{
    public class ContatoNaoEncontradoException : ApiException
    {
        public ContatoNaoEncontradoException() : base(HttpStatusCode.NotFound, "Contato não encontrado.")
        {
        }
    }
}
