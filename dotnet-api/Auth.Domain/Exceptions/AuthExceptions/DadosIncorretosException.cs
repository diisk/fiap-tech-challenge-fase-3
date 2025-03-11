using Domain.Exceptions;
using System.Net;

namespace Auth.Domain.Exceptions.AuthExceptions
{
    public class DadosIncorretosException : ApiException
    {
        public DadosIncorretosException() : base(HttpStatusCode.Unauthorized, "Dados incorretos.") { }
    }
}
