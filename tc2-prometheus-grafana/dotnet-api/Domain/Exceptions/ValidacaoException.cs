using System.Net;

namespace Domain.Exceptions
{
    public class ValidacaoException : ApiException
    {
        public ValidacaoException(string mensagem) : base(HttpStatusCode.BadRequest, mensagem) { }

    }
}
